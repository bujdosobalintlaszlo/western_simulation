using System;
using System.Collections.Generic;

class HeightMapGenerator : IBiomGenerator
{
    private int rows;
    private int cols;
    private Random rand = new Random();

    public HeightMapGenerator(int rows, int cols)
    {
        this.rows = rows;
        this.cols = cols;
    }

    public int[,] GenerateHeightMap()
    {
        int[,] heightMap = new int[rows, cols];
        var peeks = SelectPeeks();

        foreach (var (x, y) in peeks)
        {
            int peakHeight = rand.Next(3, 5);
            BFSHeight(heightMap, x, y, peakHeight);
        }

        return heightMap;
    }

    private List<(int, int)> SelectPeeks()
    {
        List<(int, int)> peeks = new();
        int peekCount = rows * cols < 49
            ? 1
            : (int)Math.Round(Math.Sqrt(rows * cols), 0);

        int attempts = 0;
        int maxAttempts = peekCount * 10;

        while (peeks.Count < peekCount && attempts < maxAttempts)
        {
            var newPeek = (rand.Next(0, rows), rand.Next(0, cols));
            if (CheckDistance(newPeek, peeks))
            {
                peeks.Add(newPeek);
            }
            attempts++;
        }

        return peeks;
    }

    private bool CheckDistance((int x, int y) newPeek, List<(int x, int y)> peeks)
    {
        foreach (var peek in peeks)
        {
            int dx = newPeek.x - peek.x;
            int dy = newPeek.y - peek.y;
            double distance = Math.Sqrt(dx * dx + dy * dy);
            if (distance < 5) return false;
        }
        return true;
    }

    private void BFSHeight(int[,] heightMap, int startX, int startY, int startHeight)
    {
        Queue<(int x, int y, int h)> queue = new();
        queue.Enqueue((startX, startY, startHeight));

        while (queue.Count > 0)
        {
            var (x, y, h) = queue.Dequeue();

            if (x < 0 || x >= rows || y < 0 || y >= cols)
                continue;

            if (heightMap[x, y] >= h)
                continue;

            heightMap[x, y] = h;

            if (h <= 0)
                continue;

            queue.Enqueue((x + 1, y, h - 1));
            queue.Enqueue((x - 1, y, h - 1));
            queue.Enqueue((x, y + 1, h - 1));
            queue.Enqueue((x, y - 1, h - 1));
        }
    }

    public void PrintHeightMap(int[,] map)
    {
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                Console.Write(map[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
}
