using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using Vectrosity;

public class GridRenderer : MonoBehaviour
{
    private void Start()
    {
        Vector2 origin = Sink.Instance.transform.position;
        Vector2 halfSize = UVector.New(Sink.SizeX, Sink.SizeY) * .5f;

        for(int i = 1; i < Sink.SizeX; i++)
        {
            VectorLine.SetLine3D(Color.yellow,
                UVector.New(origin.x - halfSize.x + i, origin.y + halfSize.y),
                UVector.New(origin.x - halfSize.x + i, origin.y - halfSize.y)
                );
        }

        for(int i = 1; i < Sink.SizeY; i++)
        {
            VectorLine.SetLine3D(Color.yellow,
                UVector.New(origin.x + halfSize.x, origin.y - halfSize.y + i),
                UVector.New(origin.x - halfSize.x, origin.y - halfSize.y + i)
                );
        }
    }

    Vector2 ToScreen(Vector3 position)
    {
        return Camera.main.WorldToScreenPoint(position);
    }
}
