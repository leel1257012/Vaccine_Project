using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject draggingObject;
    public GameObject currentContainer;
    public bool empty = true;

    public static GameManager instance;

    public bool start = false;

    public int[,] array = new int[5, 10];

    private void Awake()
    {
        instance = this;
    }

    public void PlaceObject()
    {
        if(draggingObject != null && currentContainer != null)
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

            empty = false;

            currentContainer.GetComponent<ObjectContainer>().isFull = true;



            /* manage array */
            
        }
    }
}
