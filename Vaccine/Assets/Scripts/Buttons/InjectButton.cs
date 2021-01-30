using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjectButton : MonoBehaviour
{
    public GameObject COVID19, COVID19_2;
    public bool gameStart = false;

    private void OnMouseUpAsButton()
    {
        gameStart = true;
        Debug.Log("Game Start");
        COVID19.SetActive(true);
        COVID19_2.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //80 -80
        //175 -120
    }
}
