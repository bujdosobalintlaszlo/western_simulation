using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomChunk : MonoBehaviour
{
    Field[][] fields;
    public Field[][] Fields { get { return fields; } }
    public string biomType { get; private set; }
    public int Width => Fields.Length > 0 ? Fields[0].Length : 0;
    public int Height => Fields.Length;

    public BiomChunk(Field[][] fields, string biomType)
    {
        this.fields = fields;
        this.biomType = biomType;
    }


    public void ApplyGlobalOffset(int globalXOffset, int globalYOffset)
    {
        foreach (var row in Fields)
        {
            foreach (var field in row)
            {
                field.XIndex += globalXOffset;
                field.YIndex += globalYOffset;

                if (field.gameObject != null)
                {
                    field.gameObject.transform.position += new Vector3(globalXOffset, globalYOffset, 0);
                }
            }
        }
    }
}
