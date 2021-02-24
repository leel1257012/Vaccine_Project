using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject draggingObject;
    public GameObject currentContainer;
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
}
