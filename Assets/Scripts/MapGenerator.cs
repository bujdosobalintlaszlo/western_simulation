using System;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    Dictionary<string, int> fieldCount = new Dictionary<string, int>();
    public Transform mapParent;

    private System.Random rand = new System.Random();

    public static int rows = 55;
    public static int cols = 110;

    int remainingRows = rows;
    int remainingCols = cols;
    public float tileSize = 1f;

    void Start()
    {
        GenerateMap();
        CenterCamera();
    }

    public Field[][] GenerateMap()
    {
        List<Field[][]> biomChunks = new List<Field[][]>();
        int remainingRows = rows;
        int remainingCols = cols;
        int maxBiomHeight = (int)Math.Round(Math.Sqrt(rows));
        int maxBiomWidth = (int)Math.Round(Math.Sqrt(cols));

        for (int y = 0; y < rows; y += maxBiomHeight)
        {
            for (int x = 0; x < cols; x += maxBiomWidth)
            {
                int biomHeight = Math.Min(maxBiomHeight, rows - y);
                int biomWidth = Math.Min(maxBiomWidth, cols - x);
                int biomType = rand.Next(0, 7);

                Field[][] biomChunk = CreateRandomField(biomType, biomHeight, biomWidth);
                biomChunks.Add(biomChunk);
            }
        }

        return MergeBiomChunks(biomChunks);
    }

    private Field[][] MergeBiomChunks(List<Field[][]> chunks)
    {
        Debug.Log($"chunks.Count = {chunks.Count}");

        if (chunks.Count == 0)
        {
            Debug.LogError("MergeBiomChunks: chunks list is empty!");
            return null;
        }

        if (chunks[0] == null || chunks[0].Length == 0)
        {
            Debug.LogError("MergeBiomChunks: chunks[0] is null or empty!");
            return null;
        }

        if (chunks[0][0] == null)
        {
            Debug.LogError("MergeBiomChunks: chunks[0][0] is null!");
            return null;
        }


        int chunkRows = chunks[0].Length;
        int chunkCols = chunks[0][0].Length;

        // Defensive check: ensure all chunks have the same size
        foreach (var chunk in chunks)
        {
            if (chunk.Length != chunkRows || chunk[0].Length != chunkCols)
            {
                Debug.LogError("Chunks have inconsistent dimensions!");
                return null;
            }
        }

        int chunksPerRow = Mathf.CeilToInt(Mathf.Sqrt(chunks.Count));
        int chunksPerCol = Mathf.CeilToInt((float)chunks.Count / chunksPerRow);

        int mapRows = chunksPerCol * chunkRows;
        int mapCols = chunksPerRow * chunkCols;

        Field[][] map = new Field[mapRows][];
        for (int i = 0; i < mapRows; i++)
            map[i] = new Field[mapCols];

        Debug.Log($"Merging {chunks.Count} chunks into {mapRows} x {mapCols} map.");

        for (int index = 0; index < chunks.Count; index++)
        {
            int chunkX = index % chunksPerRow;
            int chunkY = index / chunksPerRow;

            Field[][] chunk = chunks[index];

            for (int row = 0; row < chunkRows; row++)
            {
                for (int col = 0; col < chunkCols; col++)
                {
                    int mapY = chunkY * chunkRows + row;
                    int mapX = chunkX * chunkCols + col;

                    if (mapY >= mapRows || mapX >= mapCols)
                    {
                        Debug.LogWarning($"Skipping field at chunk {index}, local [{row},{col}] -> out of bounds at map [{mapY},{mapX}].");
                        continue;
                    }

                    Field field = chunk[row][col];
                    if (field == null)
                    {
                        Debug.LogWarning($"Null field at chunk {index}, position [{row},{col}].");
                        continue;
                    }

                    field.XIndex = mapX;
                    field.YIndex = mapY;
                    map[mapY][mapX] = field;
                }
            }
        }

        Debug.Log("Merging complete.");
        return map;
    }



    string SelectTerrainType() {

        return "";
    }
    private Field[][] CreateRandomField(int biomType, int maxBiomHeight,int maxBiomWidth)
    {
        int height = rand.Next(5, maxBiomHeight);
        int width = rand.Next(5,maxBiomWidth);
        remainingRows -= height;
        remainingCols -= width;
        if (biomType == 0)
        {
            FlatBiom flat = new FlatBiom(height, width, 16, mapParent, rand.Next(0, 4));
            Field[][] f = flat.GenerateBiom();
            if (f.Length == 0) {
                Debug.Log(f);
            }
            return f;
        }
        else if (biomType == 1)
        {
            SlumpBiom slumpBiom = new SlumpBiom(height, width, 16, mapParent, rand.Next(0, 4));
            Field[][] f = slumpBiom.GenerateBiom();
            if (f.Length == 0)
            {
                Debug.Log(f);
            }
            return f;
        }
        else if (biomType == 2)
        {
            MurkyDesertBiom mkdesert = new MurkyDesertBiom(height, width, 16, mapParent, rand.Next(0, 4));
            Field[][] f = mkdesert.GenerateBiom();
            if (f.Length == 0)
            {
                Debug.Log(f);
            }
            return f;
        }
        else if (biomType == 3)
        {
            CactusFieldBiom cactusField = new CactusFieldBiom(height, width, 16, mapParent, rand.Next(0, 4));
            Field[][] f = cactusField.GenerateBiom();
            if (f.Length == 0)
            {
                Debug.Log(f);
            }
            return f;
        }
        else if (biomType == 4)
        {
            MountainBiom mountainBiom = new MountainBiom(height, width, 16, mapParent);
            Field[][] f = mountainBiom.GenerateBiom();
            if (f.Length == 0)
            {
                Debug.Log(f);
            }
            return f;
        }
        else if (biomType == 5)
        {
            CaosBiom caosBiom = new CaosBiom(height, width, 16, mapParent, rand.Next(0, 4));
            Field[][] f = caosBiom.GenerateBiom();
            if (f.Length == 0)
            {
                Debug.Log(f);
            }
            return f;
        }
        else if (biomType == 6)
        {
            PondBiom pondbiom = new PondBiom(height, width, 16, mapParent, rand.Next(0, 4));
            Field[][] f = pondbiom.GenerateBiom();
            if (f.Length == 0)
            {
                Debug.Log(f);
            }
            return f;
        }
        else {
            Debug.Log("Faulty biom");
            return new Field[0][];
        }
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

