using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitPlacements : MonoBehaviour, IPointerDownHandler
{ 
    public bool isFull = false;
    private GameManager gameManager;
    private LevelEditor levelEditor;
    public Image backgroundImage;
    private Color32 cur = new Color32(0, 0, 0, 83);
    public Vector2 pos;
    public int x, y;
    public UnitSpawnPoints spawnPoints;
    public List<Transform> vaccineUnits;

    public int location;
    


    private void Start()
    {
        backgroundImage = gameObject.GetComponent<Image>();

        gameManager = GameManager.instance;
        levelEditor = LevelEditor.instance;

        pos = gameObject.GetComponent<RectTransform>().anchoredPosition;
        x = (int)Mathf.Round((pos.x + 530) / 105);
        y = (int)Mathf.Round((float)((pos.y - 101.69) / (-130.31))) + 2;
        location = (x - 1) * 5 + (y - 1);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!gameManager.start && gameManager.editMode)
        {
            if (levelEditor.selected && !isFull)
            {
                isFull = true;
                levelEditor.selected = false;
                levelEditor.newLevel.arr[y - 1, x - 1] = levelEditor.selectedUnit;
                //Debug.Log(levelEditor.newLevel.arr[y - 1, x - 1]);
                Instantiate(levelEditor.unitPrefabs[levelEditor.selectedUnit],
                    spawnPoints.spawnPoints[location].transform.position,
                    Quaternion.identity, vaccineUnits[location].transform);
                //Debug.Log(location);
                //Debug.Log(temp);
                gameObject.GetComponent<Image>().color = new Color32(0, 0, 0, 0);
            }
            else if (levelEditor.selected && isFull)
            {
                levelEditor.selected = false;
                levelEditor.newLevel.arr[y - 1, x - 1] = levelEditor.selectedUnit;
                Destroy(vaccineUnits[location].transform.GetChild(0).gameObject);
                Instantiate(levelEditor.unitPrefabs[levelEditor.selectedUnit],
                    spawnPoints.spawnPoints[location].transform.position,
                    Quaternion.identity, vaccineUnits[location].transform);
            }
            else if (!levelEditor.selected && isFull)
            {
                levelEditor.newLevel.arr[y - 1, x - 1] = 0;
                Destroy(vaccineUnits[location].transform.GetChild(0).gameObject);
                isFull = false;
                gameObject.GetComponent<Image>().color = cur;

            }
        }
        
    }

    public void instantiateUnit(int unit)
    {
        /*if (gameManager == null)
            gameManager = GameManager.instance;
        if(levelEditor == null)
            levelEditor = LevelEditor.instance;*/
        Start();
        if (unit != 0)
        {
            Instantiate(levelEditor.unitPrefabs[unit],
                spawnPoints.spawnPoints[location].transform.position,
                Quaternion.identity, vaccineUnits[location].transform);
            isFull = true;
            gameObject.GetComponent<Image>().color = new Color32(0, 0, 0, 0);
        }
        else
        {
            if (gameManager.editMode)
            {
                gameObject.GetComponent<Image>().color = cur;
                isFull = false;
            }
            
        }

    }

    
}
