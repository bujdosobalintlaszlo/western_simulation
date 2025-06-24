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
        int maxBiomHeight = (int)Math.Round(Math.Sqrt(rows), 0);
        int maxBiomWidth = (int)Math.Round(Math.Sqrt(cols), 0);
        while (remainingRows > 0 && remainingCols > 0) { 
            int biomType = rand.Next(0, 7);
            Field[][] biomChunk = CreateRandomField(biomType, maxBiomHeight, maxBiomWidth);
            biomChunks.Add(biomChunk);
        }
        return MergeBiomChunks();
    }

    Field[][] MergeBiomChunks()
    {
        Field[][] map = new Field[rows][];

        return map;
    }
    string SelectTerrainType() {

        return "";
    }
    private Field[][] CreateRandomField(int biomType, int maxBiomHeight,int maxBiomWidth)
    {
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
        remainingRows -= height;
        remainingCols -= width;
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

