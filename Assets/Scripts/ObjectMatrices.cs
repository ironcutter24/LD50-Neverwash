using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public static class ObjectMatrices
{
    public enum ObjectID { Unassigned, Apribottiglie, Banana, Cup, FishThing, Glass, Pentolaccia, Pot }

    static Dictionary<string, bool[,]> matrices = new Dictionary<string, bool[,]>()
    {
        {
            ObjectID.Unassigned.ToString(),
            new bool[,]
            {
                {false, false, false, false, false },
                {false, false, false, false, false },
                {false, false, false, false, false },
                {false, false, false, false, false },
                {false, false, false, false, false },
            }
        },
        {
            ObjectID.Apribottiglie.ToString(),
            new bool[,]
            {
                {false, false, false, false, false },
                {false, true,  true,  true,  false },
                {false, false, true,  false, false },
                {false, false, true,  false, false },
                {false, false, false, false, false },
            }
        },
        {
            ObjectID.Banana.ToString(),
            new bool[,]
            {
                {false, false, false, false, false },
                {false, false, true,  false, false },
                {false, false, true,  true,  false },
                {false, false, false, false, false },
                {false, false, false, false, false },
            }
        },
        {
            ObjectID.Cup.ToString(),
            new bool[,]
            {
                {false, false, false, false, false },
                {false, false, false, false, false },
                {false, false, true,  true,  false },
                {false, false, true,  false, false },
                {false, false, false, false, false },
            }
        },
        {
            ObjectID.FishThing.ToString(),
            new bool[,]
            {
                {false, false, false, false, false },
                {false, false, false, false, false },
                {false, true,  true,  true,  false },
                {false, false, true,  false, false },
                {false, false, false, false, false },
            }
        },
        {
            ObjectID.Glass.ToString(),
            new bool[,]
            {
                {false, false, false, false, false },
                {false, false, false, false, false },
                {false, false, true,  false, false },
                {false, false, false, false, false },
                {false, false, false, false, false },
            }
        },
        {
            ObjectID.Pentolaccia.ToString(),
            new bool[,]
            {
                {false, false, false, false, false },
                {false, true,  true,  true,  false },
                {false, true,  true,  true,  false },
                {false, false, false, false, false },
                {false, false, false, false, false },
            }
        },
        {
            ObjectID.Pot.ToString(),
            new bool[,]
            {
                {false, false, false, false, false },
                {false, false, false, false, false },
                {false, true,  true,  true,  true },
                {false, false, false, false, false },
                {false, false, false, false, false },
            }
        },
    };

    public static bool[,] GetMatrix(ObjectID id)
    {
        return UMatrix.Transpose(matrices[id.ToString()]);
    }
}
