using UnityEngine;
using UnityEngine.UI;

public class Puzzle : Image
{
    Vector2 _offset;
    Texture2D _tex;
    private Texture2D AddPieceToDefault(Texture2D defaultPiece, Texture2D piece, Vector2 position)
    {
        Graphics.CopyTexture(piece, 0, 0, 0, 0, piece.width, piece.height, defaultPiece, 0, 0, (int)position.x, (int)position.y);
        Texture2D newTexture = new Texture2D(defaultPiece.width, defaultPiece.height, TextureFormat.ARGB32, false);
        Graphics.CopyTexture(defaultPiece, newTexture);
        //Color[] colors = piece.GetPixels(0, 0, piece.width, piece.height);
        //defaultPiece.SetPixels((int)position.x, (int)position.y, piece.width, piece.height, colors);
        newTexture.Apply();
        return newTexture;
    }
    
    private Vector2 GetStartPosition(PiecePosition piecePosition)
    {
        switch (piecePosition)
        {
            case PiecePosition.Top: return new Vector2(100, 200);
            case PiecePosition.Right: return new Vector2(200, 100);
            case PiecePosition.Bottom: return new Vector2(100, 0);
            case PiecePosition.Left: return new Vector2(0, 100);
            default: return Vector2.zero;
        }
    }
    private Texture2D HandleTexture(Texture2D defaultPiece, Texture2D piece, PiecePosition piecePosition)
    {
        piece = PuzzleUtil.RotateTexture(piece, piecePosition);
        defaultPiece = AddPieceToDefault(defaultPiece, piece, GetStartPosition(piecePosition));
        return defaultPiece;
    }
    public Puzzle CreatePuzzle(Texture2D defaultPiece, Texture2D topPiece, Texture2D rightPiece, Texture2D bottomPiece, Texture2D leftPiece, Vector2 offset)
    {
        _offset = offset;
        Texture2D newTexture = HandleTexture(defaultPiece, topPiece, PiecePosition.Top);
        newTexture = HandleTexture(newTexture, rightPiece, PiecePosition.Right);
        newTexture = HandleTexture(newTexture, bottomPiece, PiecePosition.Bottom);
        newTexture = HandleTexture(newTexture, leftPiece, PiecePosition.Left);
        _tex = newTexture;
        sprite = Sprite.Create(newTexture, new Rect(new Vector2(0, 0), new Vector2(newTexture.width, newTexture.height)), Vector2.zero);
        return this;
    }
}

[System.Serializable]
public class Piece
{
    public PieceType _pieceType;
    public PiecePosition _piecePosition;
    public Texture2D _tex;

    public Piece(PieceType pieceType, PiecePosition piecePosition, Texture2D tex)
    {
        _pieceType = pieceType;
        _piecePosition = piecePosition;
        _tex = PuzzleUtil.RotateTexture(tex, piecePosition);
    }
}
public enum PieceType
{
    Default = -1,
    Streight = 0,
    Inner = 1,
    Outer = 2
}
public enum PiecePosition
{
    Top = 0,
    Right = 1,
    Bottom = 2,
    Left = 3
}
