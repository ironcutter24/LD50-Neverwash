using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    Vector3 mouseOffset = Vector2.zero;

    bool isMouseOver = false;
    bool isDragged = false;

    Vector3 startPosition = Vector3.zero;

    private void Start()
    {
        startPosition = transform.position;
    }

    #region MouseOver

    private void OnMouseEnter()
    {
        transform.localScale = Vector3.one * 1.04f;
        isMouseOver = true;
    }

    private void OnMouseOver()
    {
        Debug.Log("Mouse is over GameObject.");
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
            isDragged = true;
        }
    }

    private void OnMouseDrag()
    {
        Debug.Log("Dragging...");

        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + mouseOffset;
    }

    private void OnMouseUp()
    {
        if (isDragged)
        {
            if (Sink.IsMouseOver)
            {
                startPosition = Sink.GridToWorldPosition;
                transform.position = startPosition;
            }
            else
                transform.position = startPosition;

            isDragged = false;
        }
    }

    #endregion
}
