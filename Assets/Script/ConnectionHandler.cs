using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionHandler : MonoBehaviour
{
    private static bool isVal;
    public static List<Connection> connections = new List<Connection>();
    private static int RandomizeValue()
    {
        isVal = !isVal;
        return isVal ? 1 : 2;
    }
    public static void Init(int xLength, int yLength)
    {
        for(int i = 0; i < xLength; i++)
        {
            for(int j=0; j < yLength; j++)
            {
                if (i != 0)
                {
                    connections.Add(new Connection(i - 1, i, false, RandomizeValue()));
                }
                if (j != 0)
                {
                    connections.Add(new Connection(j - 1, j, true, RandomizeValue()));
                }
            }
        }
    }
    public static PieceType GetValue(int primarId, int secoundId, bool isHorizontal)
    {
        bool isSwitch = primarId > secoundId;
        if(isSwitch)
        {
            int temp = primarId;
            primarId = secoundId;
            secoundId=temp;
        }
        PieceType val = (PieceType)connections.Find(c => c.isHorizontal == isHorizontal && c.id0 == primarId && c.id1 == secoundId).val;
        val = isSwitch ? (val== PieceType.Inner ? PieceType.Outer : PieceType.Inner) : val;
        return val;
    }
        
}
public struct Connection
{
    public int id0;
    public int id1;
    public bool isHorizontal;
    public int val;

    public Connection(int id0, int id1, bool isHorizontal, int val)
    {
        this.id0 = id0;
        this.id1 = id1;
        this.isHorizontal = isHorizontal;
        this.val = val;
    }
}
