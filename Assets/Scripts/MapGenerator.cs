using System;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public Transform mapParent;
    private System.Random rand = new System.Random();

    public static int rows = 55;
    public static int cols = 110;
    public float tileSize = 1f;

    void Start()
    {
        GenerateMap();
        CenterCamera();
    }

    public Field[][] GenerateMap()
    {
        Field[][] map = new Field[rows][];
        int maxBiomHeight = Mathf.FloorToInt(Mathf.Sqrt(rows)); // ~7
        int maxBiomWidth = Mathf.FloorToInt(Mathf.Sqrt(cols));  // ~10

        for (int y = 0; y < rows;)
        {
            int biomHeight = Mathf.Min(rand.Next(5, maxBiomHeight + 1), rows - y);
            int rowStart = y;

            for (int x = 0; x < cols;)
            {
                int biomWidth = Mathf.Min(rand.Next(5, maxBiomWidth + 1), cols - x);
                int colStart = x;

                int biomType = rand.Next(0, 7);
                Field[][] biomChunk = CreateRandomField(biomType, biomHeight, biomWidth);

                for (int i = 0; i < biomChunk.Length; i++)
                {
                    if (rowStart + i >= rows) break;

                    if (map[rowStart + i] == null)
                        map[rowStart + i] = new Field[cols];

                    for (int j = 0; j < biomChunk[i].Length; j++)
                    {
                        if (colStart + j >= cols) break;

                        Field field = biomChunk[i][j];
                        if (field == null) continue;

                        // Set field's position (centered grid)
                        field.XIndex = (colStart + j - cols / 2) * 16;
                        field.YIndex = (rows / 2 - (rowStart + i)) * 16;

                        map[rowStart + i][colStart + j] = field;
                    }
                }

                x += biomWidth;
            }

            y += biomHeight;
        }

        return map;
    }

    private Field[][] CreateRandomField(int biomType, int height, int width)
    {
        Field[][] f = null;
        switch (biomType)
        {
            case 0:
                f = new FlatBiom(height, width, 16, mapParent, rand.Next(0, 4)).GenerateBiom();
                break;
            case 1:
                f = new SlumpBiom(height, width, 16, mapParent, rand.Next(0, 4)).GenerateBiom();
                break;
            case 2:
                f = new MurkyDesertBiom(height, width, 16, mapParent, rand.Next(0, 4)).GenerateBiom();
                break;
            case 3:
                f = new CactusFieldBiom(height, width, 16, mapParent, rand.Next(0, 4)).GenerateBiom();
                break;
            case 4:
                f = new MountainBiom(height, width, 16, mapParent).GenerateBiom();
                break;
            case 5:
                f = new VoidBiom(height, width, 16, mapParent, rand.Next(0, 4)).CircularVoid();
                break;
            case 6:
                f = new PondBiom(height, width, 16, mapParent, rand.Next(0, 4)).GenerateBiom();
                break;
            default:
                Debug.LogWarning("Invalid biomType");
                return new Field[height][];
        }

        if (f == null || f.Length == 0)
            Debug.LogWarning($"Empty biom generated: type {biomType}");

        return f;
    }

    private void CenterCamera()
    {
        float camX = 0f;
        float camY = 0f;
        float camZ = -10f;

        Camera.main.transform.position = new Vector3(camX, camY, camZ);
        Camera.main.orthographicSize = (rows * tileSize) / 2f;
    }
}
