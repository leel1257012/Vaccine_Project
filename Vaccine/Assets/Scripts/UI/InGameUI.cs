using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{

    public GameObject InjectionUI;
    public GameObject virusSpawner;
    public GameObject emptyPanel;
    public GameObject editPanel;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;
    }

    #region MainGameUI
    public Button TimeStampButton;
    public Button InjectButton;

    public void openInjectPanel()
    {
        InjectionUI.SetActive(true);
        TimeStampButton.interactable = false;
        InjectButton.interactable = false;
        gameManager.panelOpened = true;
    }

    public void inject()
    {
        if(gameManager.empty == 0)
        {
            TimeStampButton.interactable = false;
            InjectButton.interactable = false;
            emptyPanel.SetActive(true);
            gameManager.panelOpened = true;
        }
        else
        {
            gameManager.start = true;
            virusSpawner.SetActive(true);
            TimeStampButton.gameObject.SetActive(false);
            InjectButton.gameObject.SetActive(false);
            if(gameManager.editMode) editPanel.gameObject.SetActive(false);
        }

    }

    public void emptyVirus()
    {
        emptyPanel.SetActive(false);
        TimeStampButton.interactable = true;
        InjectButton.interactable = true;
        gameManager.panelOpened = false;
    }


    #endregion

    #region InjectionUI
    public Button closeInjectionButton;

    public void closeButton()
    {
        InjectionUI.SetActive(false);
        TimeStampButton.interactable = true;
        InjectButton.interactable = true;
        gameManager.panelOpened = false;
    }


    #endregion

}
