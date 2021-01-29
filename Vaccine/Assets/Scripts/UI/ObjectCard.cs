using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ObjectCard : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public GameObject object_Drag;
    public GameObject object_Game;
    public Canvas canvas;
    public Image backgroundImage;
    private GameObject objectDragInstance;
    private GameManager gameManager;
    public Virus virus;

    private void Start()
    {
        gameManager = GameManager.instance;
    }

    public void OnDrag(PointerEventData eventData)
    {
        objectDragInstance.transform.position = Input.mousePosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        objectDragInstance = Instantiate(object_Drag, canvas.transform);
        objectDragInstance.transform.position = Input.mousePosition;
        objectDragInstance.GetComponent<objectDragging>().card = this;

        gameManager.draggingObject = objectDragInstance;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        gameManager.PlaceObject();
        gameManager.draggingObject = null;
        Destroy(objectDragInstance);
    }
}
