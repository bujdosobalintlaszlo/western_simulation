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
        VoidBiom voidBiom = new VoidBiom(5,5,16,mapParent,3);
        SlumpBiom slmb = new SlumpBiom(31, 15, 16, mapParent, 2);
        Field[][] map = mpgen.GenerateMap();
        Field[][] voidPiece = voidBiom.GenerateBiom();
        Field[][] intPiece = slmb.GenerateBiom();
        Debug.Log("RUN");
        /*
        for (int i = 0; i < map.Length; i++)
        {
            for (int j = 0; j < map[i].Length; j++)
            {
                if (map[i][j] != null)
                    map[i][j].DisplayField();
                else
                    Debug.LogWarning($"Field at {i},{j} is null");
            }
        }
        */
        
        for (int i = 0; i < intPiece.Length; i++)
        {
            for (int j = 0; j < intPiece[i].Length; j++)
            {
                if (intPiece[i][j] != null)
                    intPiece[i][j].DisplayField();
                else
                    Debug.LogWarning($"Field at {i},{j} is null");
            }
        }
    }


}
