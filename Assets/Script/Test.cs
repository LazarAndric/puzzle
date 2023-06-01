using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public Texture2D Tex2D;
    private void Start()
    {
        Tex2D = PuzzleUtil.RotateTexture(Tex2D, PiecePosition.Right);
        GetComponent<Image>().sprite = Sprite.Create(Tex2D, new Rect(0, 0, Tex2D.width, Tex2D.height), Vector2.zero);
    }
}
