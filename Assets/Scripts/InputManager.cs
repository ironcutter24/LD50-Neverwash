using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static Vector3 PointerPos { get { return Camera.main.ScreenToWorldPoint(Input.mousePosition); } }

    private void Update()
    {
        if(Draggable.Current != null)
        {
            if (Input.GetKeyDown(KeyCode.Q)) Draggable.Current.Rotate(-1);
            if (Input.GetKeyDown(KeyCode.E)) Draggable.Current.Rotate(1);
        }
    }
}
