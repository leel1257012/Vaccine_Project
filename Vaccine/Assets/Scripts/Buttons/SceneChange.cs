using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public string stageName;
    public GameObject stageNameObject;

    public void call()
    {
        DontDestroyOnLoad(stageNameObject);
        SceneManager.LoadScene("InGame3");
    }
}
