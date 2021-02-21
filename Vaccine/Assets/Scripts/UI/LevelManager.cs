using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelManager : MonoBehaviour
{

    GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;
    }


}
