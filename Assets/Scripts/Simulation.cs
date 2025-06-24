using System;
using UnityEngine;

public class Simulation : MonoBehaviour
{
    public Transform mapParent;
    // Start is called before the first frame update
    void Start()
    {
        MapGenerator mpgen = FindObjectOfType<MapGenerator>();
        if (mpgen == null)
        {
            Debug.LogError("MapGenerator component not found in scene!");
            return;
        }
        Field[][] map = mpgen.GenerateMap();

       // Debug.Log($"Generated map size: {map.Length} rows × {map[0].Length} cols");

        for (int i = 0; i < map.Length; i++)
        {
            for (int j = 0; j < map[i].Length; j++)
            {
                if (map[i][j] != null)
                    map[i][j].DisplayField();
                else
                    Debug.LogWarning($"Null at {i},{j}");
            }
        }

    }


}
