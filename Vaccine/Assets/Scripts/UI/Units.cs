using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Units
{
    //public int spawnTime;
    public UnitType unitType;
}

public enum UnitType
{
    Empty,
    Leukocyte,
    Wall,
    RBC,
    Sticky,
    Mochi,
    Liquid_Leukocyte,
    Gluttonous_Cell,
    Apprentice_Cell,
    Not_Neuron,
    Cactus

}
//-425 -320 105
//232 101.69 130.31

