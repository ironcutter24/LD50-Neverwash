using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    [SerializeField] ObjectMatrices.ObjectID objectID;

    [SerializeField] bool isBonusItem = false;

    bool[,] grid = new bool[5, 5];

    private static Draggable current = null;
    public static Draggable Current { get { return current; } }

    bool isMouseOver = false;
    Vector3 mouseOffset = Vector2.zero;
    Vector3 startPosition = Vector3.zero;

    Vector2Int? gridPos = null;
    int rotation = 0;
    int newRotation = 0;

    SpriteRenderer sprite;
    int spriteDryLayer = 0;
    int spriteWetLayer = -5;

    private void Awake()
    {
        grid = ObjectMatrices.GetMatrix(objectID); 
    }

    private void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        startPosition = transform.position;
    }

    #region MouseOver

    private void OnMouseEnter()
    {
        if (GameManager.IsGameOver)
            return;

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
        if (isMouseOver && !GameManager.IsGameOver)
        {
            newRotation = rotation;
            mouseOffset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            current = this;

            if (gridPos != null)
                Sink.RemoveFromGrid((Vector2)gridPos, UMatrix.Rotate(grid, rotation));

            sprite.sortingOrder = spriteDryLayer;
        }
    }

    private void OnMouseDrag()
    {
        if (!GameManager.IsGameOver)
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + mouseOffset;
    }

    private void OnMouseUp()
    {
        if (current == this)
        {
            if (Sink.IsMouseOver && !HasCollision(UMatrix.Rotate(grid, newRotation)))
            {
                // Can place

                if (gridPos != null)
                    Sink.RemoveFromGrid((Vector2)gridPos, UMatrix.Rotate(grid, rotation));
                else
                    GameManager.ResetTimer();

                startPosition = Sink.GridToWorldPosition;
                transform.position = startPosition;

                rotation = newRotation;
                SetRotation(rotation);

                gridPos = Sink.DraggedPosInGrid;
                Sink.InsertInGrid((Vector2)gridPos, UMatrix.Rotate(grid, rotation));

                if (isBonusItem)
                    ApplyBonusEffect(startPosition);

                sprite.sortingOrder = spriteWetLayer;
            }
            else
            {
                // Cannot place

                if (gridPos != null)
                {
                    Sink.InsertInGrid((Vector2)gridPos, UMatrix.Rotate(grid, rotation));
                    sprite.sortingOrder = spriteWetLayer;
                }

                SetRotation(rotation);
                transform.position = startPosition;
            }

            current = null;
        }
    }

    #endregion

    void ApplyBonusEffect(Vector2 pos)
    {
        var colliders = Physics2D.OverlapBoxAll(pos, new Vector2(2.8f, 2.8f), 0f);

        foreach(var c in colliders)
        {
            var obj = c.GetComponent<Draggable>();
            if (obj != null)
            {
                Sink.RemoveFromGrid((Vector2)obj.gridPos, UMatrix.Rotate(obj.grid, obj.rotation));
                Destroy(obj.gameObject);
            }
        }
    }

    public void PreviewRotation(int signedRotation)
    {
        newRotation += signedRotation;
        SetRotation(newRotation);
    }

    private void SetRotation(int signedRotation)
    {
        transform.rotation = Quaternion.Euler(0f, 0f, -90f * signedRotation);
        //mouseOffset = Quaternion.AngleAxis(-90f * signedRotation, Vector3.forward) * mouseOffset;
    }

    bool HasCollision(bool[,] grid)
    {
        Vector2Int cc = Sink.DraggedPosInGrid;

        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 5; x++)
            {
                try
                {
                    if (grid[x, 4 - y] && Sink.Grid[x + cc.x - 2, y + cc.y - 2] > 0)
                        return true;
                }
                catch //(System.IndexOutOfRangeException e)
                {
                    if (grid[x, 4 - y] == true)
                        return true;
                }
            }
        }
        return false;
    }
}
