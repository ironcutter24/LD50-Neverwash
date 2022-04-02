using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static Vector3 PointerPos { get { return Camera.main.ScreenToWorldPoint(Input.mousePosition); } }

    private void Update()
    {
        if(Draggable.current != null)
        {
            if (Input.GetKeyDown(KeyCode.Q)) Draggable.current.Rotate(-1);
            if (Input.GetKeyDown(KeyCode.E)) Draggable.current.Rotate(1);
        }
    }
}
