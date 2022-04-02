using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using Utility.Patterns;

public class Sink : Singleton<Sink>
{
    static int width = 10;
    static int height = 6;

    int[,] field = new int[width, height];

    Rect bounds;
    

    int PosX { get { return (int)(transform.position.x - transform.localScale.x * .5f); } }
    int PosY { get { return (int)(transform.position.y - transform.localScale.y * .5f); } }

    int SizeX { get { return (int)(transform.localScale.x); } }
    int SizeY { get { return (int)(transform.localScale.y); } }

    public static bool IsMouseOver { get { return _instance.bounds.Contains(InputManager.PointerPos); } }

    public static Vector3 GridToWorldPosition
    {
        get
        {
            return _instance.bounds.min + Vector2.one * .5f + (Vector2)MousePosInGrid;
        }
    }

    public static Vector2Int MousePosInGrid
    {
        get
        {
            var pos = InputManager.PointerPos - UVector.New(_instance.bounds.xMin, _instance.bounds.yMin, 0f);
            return new Vector2Int((int)pos.x, (int)pos.y);
        }
    }

    private void Start()
    {
        bounds = new Rect(PosX, PosY, SizeX, SizeY);
    }
}
