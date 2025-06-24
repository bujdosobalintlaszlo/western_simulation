using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Biom
{
    private int rows;
    private int cols;
    private float tileSize;
    private Transform mapParent;

    public int Rows { get { return rows; } set { rows = value; } }
    public int Cols { get { return cols; } set { cols = value; } }
    public float TileSize { get { return tileSize; } set { tileSize = value; } }
    public Transform MapParent { get { return mapParent; } set { mapParent = value; } }
    public Biom(int rows, int cols, float tileSize, Transform mapParent)
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
