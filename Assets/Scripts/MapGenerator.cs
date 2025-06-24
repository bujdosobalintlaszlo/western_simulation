using System;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    Dictionary<string, int> fieldCount = new Dictionary<string, int>();
    public Transform mapParent;

    private System.Random rand = new System.Random();

    public int rows = 55;

    public int cols = 110;

    public float tileSize = 1f;

    void Start()
    {
        GenerateMap();
        CenterCamera();
    }
    
    public Field[][] GenerateMap()
    {
        Field[][] map = new Field[rows][];
        Field[][] subMap = new Field[rows][];
        float offsetX = (cols - 1) * tileSize / 2f;
        float offsetY = (rows - 1) * tileSize / 2f;
        
        for (int i = 0; i < rows; i++)
        {
            map[i] = new Field[cols];

            for (int j = 0; j < cols; j++)
            {
                Field[][] biomChunk = CreateRandomField(i, j, offsetX, offsetY);
                map[i][j] = field;
            }
        }

        return map;
    }
    string SelectTerrainType() {

        return "";
    }
    private Field[][] CreateRandomField(int i, int j, float offsetX, float offsetY)
    {
        int biomType = rand.Next(0, 7);
        GameObject fieldObj = new GameObject($"Field_{i}_{j}");
        fieldObj.transform.SetParent(mapParent, false);
        int maxBiomHeight = (int)Math.Round(Math.Sqrt(rows), 0);
        int maxBiomWidth = (int)Math.Round(Math.Sqrt(cols), 0);
        float posX = j * tileSize - offsetX;
        float posY = -(i * tileSize - offsetY);
        fieldObj.transform.position = new Vector3(posX, posY, 0);
        Field[][] biomChunk = null;
        int height = rand.Next(5, maxBiomHeight);
        int width = rand.Next(5,maxBiomWidth);
        if (biomType == 0)
        {
            FlatBiom flat = new FlatBiom(height, width, 16, mapParent, rand.Next(0, 4));
            biomChunk = flat.GenerateBiom();
        }
        else if (biomType == 1)
        {
            SlumpBiom slumpBiom = new SlumpBiom(height, width, 16, mapParent, rand.Next(0, 4));
            biomChunk = slumpBiom.GenerateBiom();
        }
        else if (biomType == 2)
        {
            MurkyDesertBiom mkdesert = new MurkyDesertBiom(height, width, 16, mapParent, rand.Next(0, 4));
            biomChunk=mkdesert.GenerateBiom();
        }
        else if (biomType == 3)
        {
            CactusFieldBiom cactusField = new CactusFieldBiom(height, width, 16, mapParent, rand.Next(0, 4));
            biomChunk = cactusField.GenerateBiom();
        }
        else if (biomType == 4)
        {
            MountainBiom mountainBiom = new MountainBiom(height, width, 16, mapParent);
            biomChunk = mountainBiom.GenerateBiom();
        }
        else if (biomType == 5)
        {
            CaosBiom caosBiom = new CaosBiom(height, width, 16, mapParent, rand.Next(0, 4));
            biomChunk = caosBiom.GenerateBiom();
        }
        else if (biomType == 6)
        {
            PondBiom pondbiom = new PondBiom(height, width, 16, mapParent, rand.Next(0, 4));
            biomChunk = pondbiom.GenerateBiom();
        }
        else {
            Debug.Log("Faulty biom");
            return new Field[0][];
        }
        rows -= height;
        cols -= width;
        return biomChunk;
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

