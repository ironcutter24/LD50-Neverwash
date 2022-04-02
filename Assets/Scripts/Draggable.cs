using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Draggable : SerializedMonoBehaviour
{
    [Header("Grid")]
    [SerializeField] bool[,] grid = new bool[5, 5];

    public static Draggable current = null;

    Vector3 mouseOffset = Vector2.zero;

    bool isMouseOver = false;

    Vector3 startPosition = Vector3.zero;

    private void Start()
    {
        startPosition = transform.position;
    }

    #region MouseOver

    private void OnMouseEnter()
    {
        transform.localScale = Vector3.one * 1.08f;
        isMouseOver = true;
    }

    private void OnMouseExit()
    {
        transform.localScale = Vector3.one;
        isMouseOver = false;
    }

    #endregion

    #region MouseClick

    private void OnMouseDown()
    {
        if (isMouseOver)
        {
            mouseOffset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            current = this;
        }
    }

    private void OnMouseDrag()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + mouseOffset;
    }

    private void OnMouseUp()
    {
        if (current == this)
        {
            if (Sink.IsMouseOver && !HasCollision())
            {
                startPosition = Sink.GridToWorldPosition;
                transform.position = startPosition;
                return;
            }
            else
                transform.position = startPosition;

            current = null;
        }
    }

    #endregion

    public void Rotate(int signedRotation)
    {
        transform.Rotate(0f, 0f, -90f * signedRotation);
        grid = UMatrix.RotateMatrix(grid, signedRotation);
    }

    bool HasCollision()
    {
        Vector2Int cc = Sink.DraggedPosInGrid;

        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 5; x++)
            {
                try
                {
                    if (grid[x, 4-y] && Sink.Grid[x + cc.x - 2, y + cc.y - 2] > 0)
                        return true;
                }
                catch
                {
                    if (grid[x, 4-y] == true)
                        return true;
                }
            }
        }
        return false;
    }
}
