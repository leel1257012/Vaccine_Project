using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edit : MonoBehaviour
{

    GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.instance;
        gameManager.editMode = true;
    }


}
