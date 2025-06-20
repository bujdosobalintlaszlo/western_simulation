using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Field : MonoBehaviour
{
    int xIndex;
    int yIndex;
    int zIndex;
    string type;
    bool explored;
    bool walkable;

    public int XIndex { get { return xIndex; } set { xIndex = value;  } }
    public int YIndex { get { return yIndex; } set { yIndex = value;  } }
    public int ZIndex { get { return zIndex; } set { zIndex = value;  } }
    public string Type { get { return type; } set { type = value;  } }
    public bool Explored { get { return explored; } set { explored = value;  } }

    public Field(int xIndex,int yIndex,int zIndex,string type,bool walkable)
    {
        this.xIndex = xIndex;
        this.yIndex = yIndex;
        this.zIndex = zIndex;
        this.type = type;
        explored = false;
        this.walkable = walkable;
    }

    public abstract void DisplayField();
    public override string ToString()
    {
        return $"(BASE TOSTRING)This field has {type}, and it's position is [x: {xIndex}, y: {yIndex}, z: {zIndex}";
    }
}
