using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using Utility.Patterns;

public class Sink : Singleton<Sink>
{
    static int width = 10;
    static int height = 6;

    [Header("Grid")]
    [SerializeField] int[,] grid = new int[width, height];

    Rect bounds;
    public static Rect Bounds { get { return _instance.bounds; } }


    int PosX { get { return (int)(transform.position.x - transform.localScale.x * .5f); } }
    int PosY { get { return (int)(transform.position.y - transform.localScale.y * .5f); } }

    public static int SizeX { get { return (int)(_instance.transform.localScale.x); } }
    public static int SizeY { get { return (int)(_instance.transform.localScale.y); } }

    public static bool IsMouseOver { get { return _instance.bounds.Contains(InputManager.PointerPos); } }

    public static Vector3 GridToWorldPosition { get { return _instance.bounds.min + Vector2.one * .5f + (Vector2)DraggedPosInGrid; } }

    public static Vector2Int MousePosInGrid
    {
        get
        {
            var pos = InputManager.PointerPos - UVector.New(_instance.bounds.xMin, _instance.bounds.yMin, 0f);
            return new Vector2Int((int)pos.x, (int)pos.y);
        }
    }

    public static Vector2Int DraggedPosInGrid
    {
        get
        {
            var pos = Draggable.current.transform.position - UVector.New(_instance.bounds.xMin, _instance.bounds.yMin, 0f);
            return new Vector2Int((int)pos.x, (int)pos.y);
        }
    }

    protected override void Awake()
    {
        base.Awake();
        bounds = new Rect(PosX, PosY, SizeX, SizeY);
    }

    public static void InsertInGrid()
    {

    }

    public static void RemoveFromGrid()
    {

    }
    
    /*
    T[,] RotateMatrix<T>(T[,] matrix, int signedRotation)
    {
        
    }
    
    */
    T[,] Transpose<T>(T[,] matrix)
    {
        T[,] transposed = new T[5, 5];
        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 5; x++)
                transposed[y, x] = matrix[x, y];
        }
        return transposed;
    }

    T[,] InvertRows<T>(T[,] matrix)
    {
        T[,] inverted = new T[5, 5];
        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 5; x++)
                inverted[5 - x, y] = matrix[x, y];
        }
        return inverted;
    }

    T[,] InvertColumns<T>(T[,] matrix)
    {
        T[,] inverted = new T[5, 5];
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
                inverted[x, 5 - y] = matrix[x, y];
        }
        return inverted;
    }
}