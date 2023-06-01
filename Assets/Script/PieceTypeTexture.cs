using UnityEngine;

[System.Serializable]
public struct PieceTypeTexture
{
    public PieceType PieceType;
    public Texture2D Tex;

    public PieceTypeTexture(PieceType pieceType, Texture2D tex)
    {
        PieceType = pieceType;
        Tex = tex;
    }
}
