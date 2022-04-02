using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static Vector3 PointerPos { get { return Camera.main.ScreenToWorldPoint(Input.mousePosition); } }
}
