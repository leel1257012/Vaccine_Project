using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverZone : MonoBehaviour
{
    public GameObject gameOverImg;
    public Button MainButton;

    private void Start()
    {
        gameOverImg = GameObject.Find("LevelManager").GetComponent<LevelManager>().gameOver;
        gameOverImg.SetActive(false);
        MainButton = GameObject.Find("LevelManager").GetComponent<LevelManager>().mainButton;
        MainButton.gameObject.SetActive(false);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
        gameOverImg.SetActive(true);
        MainButton.gameObject.SetActive(true);
        //Debug.Log("Infected!");
    }
}
