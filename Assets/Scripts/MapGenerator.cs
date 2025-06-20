using System;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    Dictionary<string, int> fieldCount = new Dictionary<string, int>();
    public Transform mapParent;

    private System.Random rand = new System.Random();

    public int rows = 10;

    public int cols = 10;

    public float tileSize = 1f;

    void Start()
    {
        GenerateMap();
        CenterCamera();
    }

    public Field[][] GenerateMap()
    {
        Field[][] map = new Field[rows][];

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

    private Field CreateRandomField(int i, int j, float offsetX, float offsetY)
    {
        int fieldType = rand.Next(0, 12);
        GameObject fieldObj = new GameObject($"Field_{i}_{j}");
        fieldObj.transform.SetParent(mapParent, false);

        // Correct tile positioning in world space
        float posX = j * tileSize - offsetX;
        float posY = -(i * tileSize - offsetY);
        fieldObj.transform.position = new Vector3(posX, posY, 0);

        // Add and configure correct field component
        Field field = fieldType switch
        {
            0 => fieldObj.AddComponent<BaseTerrain>(),
            1 => fieldObj.AddComponent<Boots>(),
            2 => fieldObj.AddComponent<Rock>(),
            3 => fieldObj.AddComponent<Water>(),
            4 => fieldObj.AddComponent<Cactus>(),
            5 => fieldObj.AddComponent<SilverBullet>(),
            6 => fieldObj.AddComponent<EmptyField>(),
            7 => fieldObj.AddComponent<Gold>(),
            8 => fieldObj.AddComponent<QuickSand>(),
            9 => fieldObj.AddComponent<TownHall>(),
            10 => fieldObj.AddComponent<Whiskey>(),
            _ => fieldObj.AddComponent<BaseTerrain>(),
        };

        field.Type = fieldType == 1 ? "base" :
                     fieldType == 2 ? "rock" :
                     fieldType == 3 ? "water" : "default";

        // ✅ Clean, centered indexing logic:
        int centerRow = rows / 2;
        int centerCol = cols / 2;

        field.XIndex = (j - centerCol) * 16;
        field.YIndex = (centerRow - i) * 16; // flipped Y so top = max index
        field.ZIndex = 0;

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

