using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject draggingObject;
    public GameObject currentContainer;
    public GameObject success;
    public Text virusText;
    public int virusCount;
    public int empty;
    public bool editMode = false;

    public static GameManager instance;

    public bool start = false;
    public bool panelOpened = false;

    public int[,] array = new int[5, 10];

    private void Awake()
    {
        instance = this;
        empty = 0;
    }

    private void Update()
    {
        virusText.text = virusCount.ToString();
        if(start && virusCount <= 0)
        {
            start = false;
            success.gameObject.SetActive(true);
        }
    }

    public void PlaceObject()       // placing virus
    {
        if(draggingObject != null && currentContainer != null &&
            (currentContainer.GetComponent<ObjectContainer>().editable == true || editMode == true))
        {

            //Debug.Log(currentContainer.transform.position);
            currentContainer.GetComponent<Image>().sprite 
                = draggingObject.GetComponent<Image>().sprite;

            currentContainer.GetComponent<Image>().color 
                = draggingObject.GetComponent<Image>().color;

            currentContainer.GetComponent<Image>().preserveAspect = true;

            //Instantiate(draggingObject.GetComponent<objectDragging>().card.object_Game, 
            //currentContainer.transform);

            int x = currentContainer.GetComponent<ObjectContainer>().x - 1;
            int y = currentContainer.GetComponent<ObjectContainer>().y - 1;

            array[y, x] = (int)draggingObject.GetComponent<objectDragging>().card.virus.virustype;

            empty++;

            currentContainer.GetComponent<ObjectContainer>().isFull = true;



            /* manage array */
            
        }
    }

    public void countVirus()
    {
        for(int i=0; i<5; i++)
        {
            for(int j=0; j<10; j++)
            {
                if (array[i, j] != 0 && array[i, j] != -1) virusCount++;
            }
        }
    }

    public void returnMenu()
    {
        SceneManager.LoadScene("ImmuneSelectScene");
    }
}
