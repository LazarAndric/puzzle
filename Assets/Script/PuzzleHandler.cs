using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class PuzzleHandler : MonoBehaviour
{
    const int DEFAULT_PUZZLE = 150;
    public Transform PuzzleContainer;
    public PuzzleHolder PuzzleHolder;
    public Texture2D Puzzle;
    public List<PieceTypeTexture> PieceTypeTextures = new List<PieceTypeTexture>();
    public List<Puzzle> Puzzles = new List<Puzzle>();

    public Texture2D GetTextureByType(PieceType piece) => PieceTypeTextures.Find(p => p.PieceType == piece).Tex;
    public Piece CreatePiece(PieceType pieceType, PiecePosition piecePosition) => new Piece(pieceType, piecePosition, GetTextureByType(pieceType));
    public void MovePuzzle(Transform puzzle,Vector2 position)
    {
        puzzle.SetParent(PuzzleContainer, true);
        puzzle.localPosition = position;
    }
    public void CreatePuzzles(Texture2D texture)
    {
        int size = texture.width != texture.height ? PuzzleUtil.GCD(texture.width, texture.height) : DEFAULT_PUZZLE;
        size =(int)(size/ 1.5f);
        Vector2 vector2 = new Vector2(texture.width/size, texture.height/size);
        ConnectionHandler.Init((int)vector2.x, (int)vector2.y);
        List<Puzzle> puzzles=PuzzleHolder.GetPuzzles((int)(vector2.x * vector2.y));
        int index = 0;
        for (int i = 0; i < vector2.x; i++)
        {
            for(int j = 0; j < vector2.y; j++)
            {
                Vector2 offset = new Vector2(j, i) * size;
                Texture2D topPiece = GetTextureByType(i + 1 >= vector2.x ? PieceType.Streight : ConnectionHandler.GetValue(i, i+1, false));
                Texture2D rightPiece = GetTextureByType(j  + 1 >= vector2.y ? PieceType.Streight : ConnectionHandler.GetValue(j, j + 1, true));
                Texture2D bottomPiece = GetTextureByType(i - 1 < 0 ? PieceType.Streight : ConnectionHandler.GetValue(i, i - 1, false));
                Texture2D leftPiece = GetTextureByType(j - 1 < 0 ? PieceType.Streight : ConnectionHandler.GetValue(j, j - 1, true));
                MovePuzzle(puzzles[index].transform, offset);
                Puzzles.Add(puzzles[index++].CreatePuzzle(GetTextureByType(PieceType.Default), topPiece, rightPiece, bottomPiece, leftPiece, offset));
            }
        }
    }

    private void Start()
    {
        PuzzleHolder.InitPuzzles();
        CreatePuzzles(Puzzle);
    }
}
