using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class VoidBiom : IBiomGenerator
{
    private System.Random rand = new System.Random();

    private int rows;
    private int cols;
    private float tileSize;
    private Transform mapParent;
    int voidLevel;

    public VoidBiom(int rows, int cols, float tileSize, Transform mapParent, int voidLevel)
    {
        this.rows = rows;
        this.cols = cols;
        this.tileSize = tileSize;
        this.mapParent = mapParent;
        this.voidLevel = voidLevel;
    }

    public Field[][] GenerateBiom()
    {
        Field[][] biomeFields = new Field[rows][];
        int voidType = rand.Next(1, 100);
        float voidProbability = voidLevel switch
        {
            3 => 1f,
            2 => 0.5f,
            1 => 0.25f,
            _ => 0f
        };

        float offsetX = (cols * tileSize) / 2f;
        float offsetY = (rows * tileSize) / 2f;
        if (voidLevel == 1)
        {
            return SmileVoid();
        }
        else if (voidLevel > 1 && voidLevel < 41)
        {
            return MiniVoid();
        }
        else if (voidLevel > 40 && voidLevel < 61)
        {
            return CircularVoid();
        }
        else if (voidLevel > 60 && voidLevel < 71)
        {
            return RandomVoid();
        }
        else if (voidLevel > 70 && voidLevel < 86)
        {
            return RectengularVoid();
        }
        else if (voidLevel > 85 && voidLevel < 96)
        {
            return HalfCircleVoid();
        }
        else { 
            return FullVoid();
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


    public Field[][] CircularVoid()
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

                bool isVoid = dist <= radius;

                line[j] = CreateField(i, j, offsetX, offsetY, isVoid);
            }

            fields[i] = line;
        }

        return fields;
    }

    public Field[] HalfCircleVoid() {
        if (rows % 2 == 0)
        {

        }
        else { 
            
        }
    }

    public Field[][] RectengularVoid()
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
                    bool isVoid = j >= voidStartIndex && j <= voidEndIndex;
                    line[j] = CreateField(i, j, offsetX, offsetY, isVoid);
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
                    bool isVoid = j >= voidStartIndex && j <= voidEndIndex;
                    line[j] = CreateField(i, j, offsetX, offsetY, isVoid);
                }
                --voidStartIndex;
                ++voidEndIndex;
                map[i] = line;
            }
        }

        return map;
    }


    public Field[][] FullVoid()
    {
        Field[][] map = new Field[rows][];
        float offsetX = (cols * tileSize) / 2f;
        float offsetY = (rows * tileSize) / 2f;
        for (int i = 0; i < rows; ++i) {
            Field[] fields = new Field[cols];
            for (int j = 0; ++j < cols; ++j) {
                fields[j] = CreateField(i,j,offsetX,offsetY,true);
            }
        }
        return map;
    }


    public Field[][] RandomizedVoid() { 
    
    }

    public Field[][] SmileVoid() 
    {
        if (rows != cols) { 
            return CircularVoid();
        }

        if (rows >= 8 && cols >= 8) { 
            
        }
    }
    public Field[][] MiniVoid() { 
    
    }
    public Field[][] RandomVoid() {
        float voidProbability = voidLevel switch
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
                bool isVoid = rand.NextDouble() < voidProbability;
                biomeFields[i][j] = CreateField(i, j, offsetX, offsetY, isVoid);
            }
        }
        return biomeFields;
    }
    //implement later
    public bool ValidateStructure(Field[][] structure) { 
        return true;
    }
}
