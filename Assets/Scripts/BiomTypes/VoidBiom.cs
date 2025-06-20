using System;
using UnityEngine;

public class BiomeGenerator
{
    private System.Random rand = new System.Random();

    private int rows;
    private int cols;
    private float tileSize;
    private Transform mapParent;

    public BiomeGenerator(int rows, int cols, float tileSize, Transform mapParent)
    {
        this.rows = rows;
        this.cols = cols;
        this.tileSize = tileSize;
        this.mapParent = mapParent;
    }

    public Field[][] GenerateBiome(int voidLevel)
    {
        Field[][] biomeFields = new Field[rows][];

        float voidProbability = voidLevel switch
        {
            3 => 1f,
            2 => 0.5f,
            1 => 0.25f,
            _ => 0f
        };

        float offsetX = (cols * tileSize) / 2f;
        float offsetY = (rows * tileSize) / 2f;

        for (int i = 0; i < rows; i++)
        {
            biomeFields[i] = new Field[cols];

            for (int j = 0; j < cols; j++)
            {
                bool isVoid = rand.NextDouble() < voidProbability;
                biomeFields[i][j] = CreateField(i, j, offsetX, offsetY, isVoid);
            }
        }

        return biomeFields;
    }

    private Field CreateField(int i, int j, float offsetX, float offsetY, bool isVoid)
    {
        int fieldType;

        if (isVoid)
        {
            fieldType = 6;
        }
        else
        {
            fieldType = rand.Next(0, 12);
            if (fieldType == 6)
                fieldType = 0; 
        }

        GameObject fieldObj = new GameObject($"Field_{i}_{j}");
        fieldObj.transform.SetParent(mapParent, false);

        float posX = j * tileSize - offsetX;
        float posY = -(i * tileSize - offsetY);
        fieldObj.transform.position = new Vector3(posX, posY, 0);

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

        field.Type = fieldType switch
        {
            1 => "boots",
            2 => "rock",
            3 => "water",
            4 => "cactus",
            5 => "silverBullet",
            6 => "emptyField",
            7 => "gold",
            8 => "quickSand",
            9 => "townHall",
            10 => "whiskey",
            _ => "baseTerrain"
        };

        int centerRow = rows / 2;
        int centerCol = cols / 2;

        field.XIndex = (j - centerCol) * 16;
        field.YIndex = (centerRow - i) * 16; // flipped Y so top = max index
        field.ZIndex = 0;

        return field;
    }
}
