using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToMainScreen : MonoBehaviour
{
    public void mainButton()
    {
        SceneManager.LoadScene("MainScene");
    }
}
