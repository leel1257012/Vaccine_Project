using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{



    #region InjectionUI

    public GameObject InjectionUI;
    public Button closeInjectionButton;

    public void closeButton()
    {
        InjectionUI.SetActive(false);
    }


    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
