using System.Collections.Generic;
using UnityEngine;

public class PuzzleHolder : MonoBehaviour
{
    public Puzzle Prefab;
    public Transform Holder;
    public int Quantity;
    private List<Puzzle> _puzzles = new List<Puzzle>();
    
    public void InitPuzzles()
    {
        for(int i=0;i< Quantity; i++)
        {
            _puzzles.Add(Instantiate(Prefab, Holder));
        }
    }
    public List<Puzzle> GetPuzzles(int quantity) => _puzzles.GetRange(0, quantity);
}