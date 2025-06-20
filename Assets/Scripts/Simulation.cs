using UnityEngine;

public class Simulation : MonoBehaviour
{
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
        Debug.Log("RUN");

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
    }


}
