using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBiomGenerator
{
    bool ValidateStructure(Field[][] structure);
    Field[][] GenerateStructure();   
}
