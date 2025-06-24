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
      
        return chunks[0];
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

