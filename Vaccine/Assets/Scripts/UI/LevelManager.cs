using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelManager : MonoBehaviour
{
    public GameObject gameOver;
    public Button mainButton;
    GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;
        
    }


}
