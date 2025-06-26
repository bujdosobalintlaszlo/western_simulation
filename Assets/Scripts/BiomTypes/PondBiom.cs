using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PondBiom : Biom, IBiomGenerator
{
    private System.Random rand = new System.Random();

    private int rows;
    private int cols;
    private float tileSize;
    private Transform mapParent;
    int slumpLevel;

    public PondBiom(int rows, int cols, float tileSize, Transform mapParent, int slumpLevel) : base(rows, cols, tileSize, mapParent)
    {
        this.rows = rows;
        this.cols = cols;
        this.slumpLevel = slumpLevel;
    }

    public Field[][] GenerateBiom()
    {
        if (slumpLevel > 0 && slumpLevel < 41)
        {
            return MiniPond();
        }
        else if (slumpLevel > 40 && slumpLevel < 61)
        {
            return CircularPond();
        }
        else if (slumpLevel > 60 && slumpLevel < 71)
        {
            return RandomPond();
        }
        else if (slumpLevel > 70 && slumpLevel < 86)
        {
            return RectengularPond();
        }
        else if (slumpLevel > 85 && slumpLevel < 96)
        {
            return HalfCirclePond();
        }
        else
        {
            return FullPond();
        }
    }

    private Field CreateField(int i, int j, float offsetX, float offsetY, bool isWater)
    {
        int fieldType;

        if (isWater)
        {
            fieldType = 2;
        }
        else
        {
            fieldType = rand.Next(0, 6);
            if (fieldType == 2)
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
            4 => fieldObj.AddComponent<QuickSand>(),
            5 => fieldObj.AddComponent<EmptyField>(),
            _ => fieldObj.AddComponent<BaseTerrain>(),
        };

        field.Type = fieldType switch
        {
            0 => "baseTerrain",
            1 => "rock",
            2 => "water",
            3 => "cactus",
            4 => "quickSand",
            5 => "emptyField",
            _ => "baseTerrain"
        };

        int centerRow = rows / 2;
        int centerCol = cols / 2;

        field.XIndex = (j - centerCol) * 16;
        field.YIndex = (centerRow - i) * 16;
        field.ZIndex = 0;

        return field;
    }


    public Field[][] CircularPond()
    {
        Field[][] fields = new Field[rows][];

        float offsetX = (cols * tileSize) / 2f;
        float offsetY = (rows * tileSize) / 2f;

        float centerRow = rows / 2f;
        float centerCol = cols / 2f;
        float radius = Math.Min(rows, cols) / 3f;

        for (int i = 0; i < rows; ++i)
        {
            Field[] line = new Field[cols];

            for (int j = 0; j < cols; ++j)
            {
                float dist = MathF.Sqrt((i - centerRow) * (i - centerRow) + (j - centerCol) * (j - centerCol));

                bool isWater = dist <= radius;

                line[j] = CreateField(i, j, offsetX, offsetY, isWater);
            }

            fields[i] = line;
        }

        return fields;
    }

    public Field[][] HalfCirclePond()
    {
        Field[][] map = new Field[rows][];
        float offsetX = (cols * tileSize) / 2f;
        float offsetY = (rows * tileSize) / 2f;

        int voidStartIndex = (int)Math.Floor((decimal)(cols / 2));
        int voidEndIndex = (int)Math.Ceiling((decimal)(cols / 2));

        for (int i = 0; i < rows; ++i)
        {
            Field[] line = new Field[cols];
            for (int j = 0; j < cols; ++j)
            {
                bool isWater = j == voidStartIndex || j == voidEndIndex;
                line[j] = CreateField(i, j, offsetX, offsetY, isWater);
            }

            --voidStartIndex;
            ++voidEndIndex;
            map[i] = line;
        }

        return map;
    }


    public Field[][] RectengularPond()
    {
        Field[][] map = new Field[rows][];
        int voidStartIndex;
        int voidEndIndex;
        float offsetX = (cols * tileSize) / 2f;
        float offsetY = (rows * tileSize) / 2f;

        if (rows % 2 == 0)
        {
            voidStartIndex = (int)Math.Floor((decimal)(cols / 2));
            voidEndIndex = (int)Math.Ceiling((decimal)(cols / 2));

            for (int i = 0; i < rows; ++i)
            {
                Field[] line = new Field[cols];
                for (int j = 0; j < cols; ++j)
                {
                    bool isWater = j >= voidStartIndex && j <= voidEndIndex;
                    line[j] = CreateField(i, j, offsetX, offsetY, isWater);
                }
                --voidStartIndex;
                ++voidEndIndex;
                map[i] = line;
            }
        }
        else
        {
            voidStartIndex = (int)Math.Floor((decimal)(cols / 2));
            voidEndIndex = voidStartIndex;

            for (int i = 0; i < rows; ++i)
            {
                Field[] line = new Field[cols];
                for (int j = 0; j < cols; ++j)
                {
                    bool isWater = j >= voidStartIndex && j <= voidEndIndex;
                    line[j] = CreateField(i, j, offsetX, offsetY, isWater);
                }
                --voidStartIndex;
                ++voidEndIndex;
                map[i] = line;
            }
        }

        return map;
    }


    public Field[][] FullPond()
    {
        Field[][] map = new Field[rows][];
        float offsetX = (cols * tileSize) / 2f;
        float offsetY = (rows * tileSize) / 2f;

        for (int i = 0; i < rows; ++i)
        {
            Field[] fields = new Field[cols];
            for (int j = 0; j < cols; ++j)
            {
                fields[j] = CreateField(i, j, offsetX, offsetY, true);
            }
            map[i] = fields;
        }

        return map;
    }

    public Field[][] MiniPond()
    {
        Field[][] map = new Field[rows][];
        float offsetX = (cols * tileSize) / 2f;
        float offsetY = (rows * tileSize) / 2f;

        int voidRow = rand.Next(Math.Min(1, rows - 1), Math.Max(2, rows - 1));
        int voidCol = rand.Next(Math.Min(1, cols - 1), Math.Max(2, cols - 1));


        for (int i = 0; i < rows; ++i)
        {
            Field[] fields = new Field[cols];
            for (int j = 0; j < cols; ++j)
            {
                bool isWater =
                    Math.Abs(i - voidRow) <= 1 &&
                    Math.Abs(j - voidCol) <= 1;

                fields[j] = CreateField(i, j, offsetX, offsetY, isWater);
            }
            map[i] = fields;
        }

        return map;
    }

    public Field[][] RandomPond()
    {
        float voidProbability = slumpLevel switch
        {
            3 => 1f,
            2 => 0.5f,
            1 => 0.25f,
            _ => 0f
        };

        float offsetX = (cols * tileSize) / 2f;
        float offsetY = (rows * tileSize) / 2f;
        Field[][] biomeFields = new Field[rows][];
        for (int i = 0; i < rows; i++)
        {
            biomeFields[i] = new Field[cols];

            for (int j = 0; j < cols; j++)
            {
                bool isWater = rand.NextDouble() < voidProbability;
                biomeFields[i][j] = CreateField(i, j, offsetX, offsetY, isWater);
            }
        }
        return biomeFields;
    }
    //implement later
    public bool ValidateStructure(Field[][] structure)
    {
        return true;
    }
}
