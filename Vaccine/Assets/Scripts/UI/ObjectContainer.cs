using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ObjectContainer : MonoBehaviour, IPointerDownHandler
{
    public bool isFull = false;
    public bool editable = true;
    public bool disabled = false;
    public GameManager gameManager;
    public Image backgroundImage;
    private Color32 cur = new Color32(0, 0, 0, 83);
    public Vector2 pos;
    public int x, y;
    public GameObject emptyCard;

    IEnumerator Start()
    {
        gameManager = GameManager.instance;

        yield return new WaitForEndOfFrame();
        pos = gameObject.GetComponent<RectTransform>().anchoredPosition;
        x = (int) Mathf.Round((pos.x + 95) / 175);
        y = (int) Mathf.Round((pos.y - 40) / (-120));
    }

    //private void Awake()
    //{
        
        
    //}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(gameManager.draggingObject != null && isFull == false && 
            (editable == true || gameManager.editMode == true) && !disabled)
        {
            gameManager.currentContainer = this.gameObject;
            backgroundImage.color = new Color32(56, 0, 0, 152);
            //backgroundImage.enabled = true;
        }   
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        gameManager.currentContainer = null;
        if(!isFull && !disabled) backgroundImage.color = cur;

        //backgroundImage.enabled = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(gameManager.draggingObject == null && isFull &&          //empty a full container
            (editable == true || gameManager.editMode == true) && !disabled)
        {
            isFull = false;
            gameManager.empty--;
            gameManager.array[y - 1, x - 1] = 0;
            backgroundImage.sprite = emptyCard.GetComponent<Image>().sprite;
            backgroundImage.color = cur;
        }

        else if (gameManager.draggingObject == null && !isFull &&        //disable an empty container
            !disabled && gameManager.editMode == true)
        {
            disabled = true;
            gameManager.array[y - 1, x - 1] = -1;
            backgroundImage.color = new Color32(255, 0, 0, 83);
        }

        else if (gameManager.draggingObject == null && !isFull &&        //enable an disabled container
            disabled && gameManager.editMode == true)
        {
            disabled = false;
            gameManager.array[y - 1, x - 1] = 0;
            backgroundImage.color = cur;
        }



    }
}
