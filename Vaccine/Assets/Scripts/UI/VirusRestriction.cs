using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirusRestriction : MonoBehaviour
{
    public GameObject virusCard;
    private LevelEditor levelEditor;
    private GameManager gameManager;
    private Text num;
    public int virusNum;
    private int counter;

    private void Start()
    {
        levelEditor = LevelEditor.instance;
        gameManager = GameManager.instance;
        counter = 0;
        if (virusNum == 1) {
            gameObject.GetComponent<Text>().text = levelEditor.loadLevel.sneeze.ToString();
            counter = levelEditor.loadLevel.sneeze;
        }
        if (virusNum == 2) {
            gameObject.GetComponent<Text>().text = levelEditor.loadLevel.cough.ToString();
            counter = levelEditor.loadLevel.cough;
        }
        if (virusNum == 3) {
            gameObject.GetComponent<Text>().text = levelEditor.loadLevel.fever.ToString();
            counter = levelEditor.loadLevel.fever;
        }
        if (virusNum == 4) {
            gameObject.GetComponent<Text>().text = levelEditor.loadLevel.snot.ToString();
            counter = levelEditor.loadLevel.snot;
        }
        if (virusNum == 5) {
            gameObject.GetComponent<Text>().text = levelEditor.loadLevel.sweat.ToString();
            counter = levelEditor.loadLevel.sweat;
        }
        
    }

    private void Update()
    {
        int temp = 0;
        for(int i=0; i<5; i++)
        {
            for(int j=0; j<10; j++)
            {

                //if (levelEditor.loadLevel.vir[i, j] != 0) continue;
                if (virusNum == 1 && gameManager.array[i, j] == 4) temp++;
                if (virusNum == 2 && gameManager.array[i, j] == 1) temp++;
                if (virusNum == 3 && gameManager.array[i, j] == 2) temp++;
                if (virusNum == 4 && gameManager.array[i, j] == 10) temp++;
                if (virusNum == 5 && gameManager.array[i, j] == 6) temp++;

            }
        }

        //Debug.Log(temp);
        gameObject.GetComponent<Text>().text = (counter - temp).ToString();
        if (counter - temp > 0) virusCard.GetComponent<ObjectCard>().exist = true;
        else virusCard.GetComponent<ObjectCard>().exist = false;

    }
}
