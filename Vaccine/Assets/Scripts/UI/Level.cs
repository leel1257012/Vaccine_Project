using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level 
{
    public int[,] arr;
    public int[,] vir;

    public int cough;
    public int fever;
    public int redEyes;
    public int sneeze;
    public int insomnia;
    public int sweat;
    public int stupor;
    public int vomit;
    public int bald;
    public int snot;

    public Level()
    {
        arr = new int[5, 8];
        vir = new int[5, 10];
        cough = 0;
        fever = 0;
        redEyes = 0;
        sneeze = 0;
        insomnia = 0;
        sweat = 0;
        stupor = 0;
        vomit = 0;
        bald = 0;
        snot = 0;
    }

}
