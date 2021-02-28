using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class ObjectCard : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject object_Drag;
    //public GameObject object_Game;
    public Canvas canvas;
    public Image backgroundImage;
    private GameObject objectDragInstance;
    private GameManager gameManager;
    public Virus virus;
    public bool exist = false;

    [SerializeField]
    private GameObject status;
    /*[SerializeField]
    private GameObject Virus_prefab;*/

    private void Start()
    {
        gameManager = GameManager.instance;
        status = GameObject.Find("Canvas").transform.Find("Status").gameObject;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(exist == true)
        {
            objectDragInstance.transform.position = Input.mousePosition;
        }
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(exist == true)
        {
            objectDragInstance = Instantiate(object_Drag, canvas.transform);
            objectDragInstance.transform.position = Input.mousePosition;
            objectDragInstance.GetComponent<objectDragging>().card = this;

            gameManager.draggingObject = objectDragInstance;
        }
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(exist == true)
        {
            gameManager.PlaceObject();
            gameManager.draggingObject = null;
            Destroy(objectDragInstance);
        }

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        status.SetActive(true);
        status.GetComponent<Transform>().position = Input.mousePosition;

        //status.GetComponent<Transform>().Find("Damage").GetComponent<TextMeshProUGUI>().text = "Damage: " + Virus_prefab.GetComponent<VirusClass>().Damage;
        //status.GetComponent<Transform>().Find("Health").GetComponent<TextMeshProUGUI>().text = "Health: " + Virus_prefab.GetComponent<VirusClass>().MaxHealth;
        //status.GetComponent<Transform>().Find("AttackSpeed").GetComponent<TextMeshProUGUI>().text = "AttackSpeed: " + Math.Round((1 / Virus_prefab.GetComponent<VirusClass>().OriAttackSpeed), 2);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        status.SetActive(false); 
    }
}
