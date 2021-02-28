using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ImmuneSelectButton : MonoBehaviour
{
    public SceneChange sc;
    public void ImmuneSelect()
    {
        if (gameObject.name == "1")
            sc.stageName = "mg1";
        else if (gameObject.name == "2")
            sc.stageName = "ws1";
        else if (gameObject.name == "3")
            sc.stageName = "SJ";
        else sc.stageName = "hj";

        sc.call();
    }
}
