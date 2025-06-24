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
                Field field = CreateRandomField(i, j, offsetX, offsetY);
                map[i][j] = field;
            }
        }

        return map;
    }
    string SelectTerrainType() {

        return "";
    }
    private Field CreateRandomField(int i, int j, float offsetX, float offsetY)
    {
        int biomType = rand.Next(0, 8);
        GameObject fieldObj = new GameObject($"Field_{i}_{j}");
        fieldObj.transform.SetParent(mapParent, false);
        int maxBiomHeight = (int)Math.Round(Math.Sqrt(rows), 0);
        int maxBiomWidth = (int)Math.Round(Math.Sqrt(cols), 0);
        float posX = j * tileSize - offsetX;
        float posY = -(i * tileSize - offsetY);
        fieldObj.transform.position = new Vector3(posX, posY, 0);

            FlatBiom fn = new FlatBiom(rand.Next(5, maxBiomHeight), rand.Next(5, maxBiomWidth), 16, mapParent, rand.Next(0, 4))
            Field[][] bm = fn.GenerateBiom();
        Biom[][] biom = biomType switch
        {
            0 =>
            1 => fieldObj.AddComponent<Boots>(),
            2 => fieldObj.AddComponent<Rock>(),
            3 => fieldObj.AddComponent<Water>(),
            4 => fieldObj.AddComponent<Cactus>(),
            5 => fieldObj.AddComponent<SilverBullet>(),
            6 => fieldObj.AddComponent<EmptyField>(),
            7 => fieldObj.AddComponent<Gold>(),
            _ => fieldObj.AddComponent<BaseTerrain>(),
        };

        field.Type = fieldType == 1 ? "base" :
                     fieldType == 2 ? "rock" :
                     fieldType == 3 ? "water" : "default";

        int centerRow = rows / 2;
        int centerCol = cols / 2;

        field.XIndex = (j - centerCol) * 16;
        field.YIndex = (centerRow - i) * 16; // flipped Y so top = max index
        field.ZIndex = rand.Next(0,5);

        return field;
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

