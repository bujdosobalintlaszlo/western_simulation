using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MountainBiom : Biom, IBiomGenerator
{
    private int rows;
    private int cols;
    private System.Random rand = new System.Random();
    int maxHeight = 4;
    Transform mapParent;
    float tileSize;

    public MountainBiom(int rows, int cols,float tileSize,Transform mapParent)
    {
        this.rows = rows;
        this.cols = cols;
        this.mapParent = mapParent;
        this.tileSize = tileSize;
    }

    public int[,] GenerateMountainMap()
    {
        int[,] map = FillEmptyMap();

        int numberOfPeaks = (int)Math.Round((decimal)(rows+cols)/2,0);
        List<(int x, int y)> peaks = new List<(int, int)>();

        for (int i = 0; i < numberOfPeaks; i++)
        {
            int px = rand.Next(2, rows - 2);
            int py = rand.Next(2, cols - 2);
            peaks.Add((px, py));
            map[px, py] = maxHeight;
        }

        Queue<(int x, int y)> queue = new Queue<(int x, int y)>();
        foreach (var (x, y) in peaks)
        {
            queue.Enqueue((x, y));
        }

        int[,] directions = new int[,] { { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 } };

        while (queue.Count > 0)
        {
            var (x, y) = queue.Dequeue();
            int currentHeight = map[x, y];

            for (int i = 0; i < 4; i++)
            {
                int nx = x + directions[i, 0];
                int ny = y + directions[i, 1];

                if (nx < 0 || ny < 0 || nx >= rows || ny >= cols)
                    continue;

                if (map[nx, ny] >= 0) 
                    continue;

                int newHeight = currentHeight - 1;
                if (newHeight < 0)
                    continue;

                map[nx, ny] = newHeight;
                queue.Enqueue((nx, ny));
            }
        }
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (map[i, j] == -1)
                    map[i, j] = 0;
            }
        }

        return map;
    }


    int[,] FillEmptyMap()
    {
        int[,] matrix = new int[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                matrix[i, j] = -1;
            }
        }
        return matrix;
    }

    public bool ValidateStructure(Field[][] structure) {
        return true;
    }
    public Field[][] GenerateBiom() {
        Field[][] fields = new Field[rows][];
        int[,] heightmap = GenerateMountainMap();
        float offsetX = (cols * tileSize) / 2f;
        float offsetY = (rows * tileSize) / 2f;
        for (int i = 0; i < rows; ++i)
        {
            Field[] fieldRow = new Field[cols];
            for (int j = 0; j < cols; ++j) {
                fieldRow[j] = CreateField(i, j, heightmap[i,j],offsetX,offsetY);
            }
            fields[i] = fieldRow;
        }
        return fields;
    }

    Field CreateField(int i, int j, int z, float offsetX, float offsetY)
    {
        GameObject fieldObj = new GameObject($"Field_{i}_{j}");
        fieldObj.transform.SetParent(mapParent, false);

        float posX = j * tileSize - offsetX;
        float posY = -(i * tileSize - offsetY); 
        fieldObj.transform.position = new Vector3(posX, posY, 0);
        var field = fieldObj.AddComponent<BaseTerrain>();
        int centerRow = rows / 2;
        int centerCol = cols / 2;

        field.XIndex = (j - centerCol) * 16;
        field.YIndex = (centerRow - i) * 16;
        field.ZIndex = z;
        field.Type = "base";

        return field;
    }

}
