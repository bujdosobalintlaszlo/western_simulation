using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//FIX NEEDED
public class SlumpBiom : IBiomGenerator
{
    private System.Random rand = new System.Random();

    private int rows;
    private int cols;
    private float tileSize;
    private Transform mapParent;
    int slumpLevel;

    public SlumpBiom(int rows, int cols, float tileSize, Transform mapParent, int slumpLevel)
    {
        this.rows = rows;
        this.cols = cols;
        this.tileSize = tileSize;
        this.mapParent = mapParent;
        this.slumpLevel = slumpLevel;
    }

    public Field[][] GenerateBiom()
    {
        Field[][] biomeFields = new Field[rows][];

        float voidProbability = slumpLevel switch
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
            fieldType = 5;
        }
        else
        {
            fieldType = rand.Next(0, 6);
            if (fieldType == 5)
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
            1 => fieldObj.AddComponent<Rock>(),
            2 => fieldObj.AddComponent<Water>(),
            3 => fieldObj.AddComponent<Cactus>(),
            4 => fieldObj.AddComponent<EmptyField>(),
            5 => fieldObj.AddComponent<QuickSand>(),
            _ => fieldObj.AddComponent<BaseTerrain>(),
        };

        field.Type = fieldType switch
        {
            0 => "baseTerrain",
            1 => "rock",
            2 => "water",
            3 => "cactus",
            4 => "emptyField",
            5 => "quickSand",
            _ => "baseTerrain"
        };

        int centerRow = rows / 2;
        int centerCol = cols / 2;

        field.XIndex = (j - centerCol) * 16;
        field.YIndex = (centerRow - i) * 16;
        field.ZIndex = 0;

        return field;
    }

    //implement later
    public bool ValidateStructure(Field[][] structure)
    {
        return true;
    }
}
