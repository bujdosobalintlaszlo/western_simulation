using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Biom : MonoBehaviour
{
    private int rows;
    private int cols;
    private float tileSize;
    private Transform mapParent;

    public int Rows { get { return rows; } }
    public int Cols { get { return cols; } }
    public float TileSize { get { return tileSize; } }
    public Transform MapParent { get { return mapParent; } }
    protected Biom(int rows, int cols, float tileSize, Transform mapParent)
    {
        this.rows = rows;
        this.cols = cols;
        this.tileSize = tileSize;
        this.mapParent = mapParent;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
