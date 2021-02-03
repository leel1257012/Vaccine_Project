using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitCard : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public GameObject object_Drag;
    //public GameObject object_Game;
    public Canvas canvas;
    public Image backgroundImage;
    private GameObject objectDragInstance;
    private GameManager gameManager;
    private LevelEditor levelEditor;
    public Units unit;

    private void Start()
    {
        levelEditor = LevelEditor.instance;
        gameManager = GameManager.instance;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //objectDragInstance.transform.position = Input.mousePosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {

        levelEditor.selectedUnit = (int)unit.unitType;
        levelEditor.selected = true;
        //Debug.Log(levelEditor.selected);
        Debug.Log(unit.unitType + " selected");
        
        
        //objectDragInstance = Instantiate(object_Drag, canvas.transform);
        //objectDragInstance.transform.position = Input.mousePosition;
        //objectDragInstance.GetComponent<objectDragging>().card = this;

        //gameManager.draggingObject = objectDragInstance;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log(levelEditor.selectedUnit);
        //gameManager.PlaceObject();
        //gameManager.draggingObject = null;
        //Destroy(objectDragInstance);
    }
}