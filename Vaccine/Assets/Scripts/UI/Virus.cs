using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Virus
{
    //public int spawnTime;
    public VirusType virustype;
}

public enum VirusType
{
    Empty,
    Virus_Cough,
    Virus_Fever,
    Virus_RedEyes,
    Virus_Sneeze,
    Virus_Insomnia,
    Virus_Sweat,
    Virus_Stupor,
    Virus_Vomit,
    Virus_Bald,
    Virus_Snot

}

