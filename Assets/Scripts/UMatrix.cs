using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UMatrix
{
    public static T[,] RotateMatrix<T>(T[,] matrix, int signedRotation)
    {
        bool isClockwise = signedRotation > 0;
        int iterations = Mathf.Abs(signedRotation);

        for (int i = 0; i < iterations; i++)
        {
            matrix = Transpose<T>(matrix);
            matrix = isClockwise ? InvertRows<T>(matrix) : InvertColumns<T>(matrix);
        }
        return matrix;
    }

    static T[,] Transpose<T>(T[,] matrix)
    {
        var sizeX = matrix.GetLength(0);
        var sizeY = matrix.GetLength(1);

        T[,] transposed = new T[sizeY, sizeX];
        for (int y = 0; y < sizeY; y++)
        {
            for (int x = 0; x < sizeX; x++)
                transposed[y, x] = matrix[x, y];
        }
        return transposed;
    }

    static T[,] InvertRows<T>(T[,] matrix)
    {
        var sizeX = matrix.GetLength(0);
        var sizeY = matrix.GetLength(1);

        T[,] inverted = new T[sizeX, sizeY];
        for (int y = 0; y < sizeY; y++)
        {
            for (int x = 0; x < sizeX; x++)
                inverted[sizeX - 1 - x, y] = matrix[x, y];
        }
        return inverted;
    }

    static T[,] InvertColumns<T>(T[,] matrix)
    {
        var sizeX = matrix.GetLength(0);
        var sizeY = matrix.GetLength(1);

        T[,] inverted = new T[sizeX, sizeY];
        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
                inverted[x, sizeY - 1 - y] = matrix[x, y];
        }
        return inverted;
    }
}
