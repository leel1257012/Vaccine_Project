using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmuneSelectButton : MonoBehaviour
{
    string stage;
    public void ImmuneSelect()
    {
        stage = gameObject.name;
        Debug.Log("Load stage" + stage);
    }
}
