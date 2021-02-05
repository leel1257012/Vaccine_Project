using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEditor;
using Newtonsoft.Json;

public class LevelEditor : MonoBehaviour
{
    public static LevelEditor instance;
    public int selectedUnit;
    public bool selected = false;
    public List<GameObject> unitPrefabs;
    public InputField stageName;
    public Level newLevel;
    public Level loadLevel;
    private Level emptyLevel;
    public List<Transform> vaccineUnits;
    public List<GameObject> placements;
    private GameManager gameManager;



    private void Awake()
    {
        instance = this;
        newLevel = ScriptableObject.CreateInstance<Level>();
        emptyLevel = ScriptableObject.CreateInstance<Level>();
        
    }

    private void Start()
    {
        gameManager = GameManager.instance;
        gameManager.editMode = true;
    }

    public void CellInstantiate(Level level)
    {
        foreach (Transform child in vaccineUnits)
        {
            if (child.childCount > 0)
            {
                Destroy(child.GetChild(0).gameObject);
            }
        }

        //int index = 0;
        for(int i=0; i<5; i++)
        {
            for(int j=0; j<8; j++)
            {
                int temp = i + j * 5;
                placements[temp].GetComponent<UnitPlacements>().instantiateUnit(level.arr[i, j]);
                newLevel.arr[i, j] = level.arr[i, j];
                
            }
        }
        
    }

    public void LoadLevel()
    {
        try
        {
            string str = File.ReadAllText(Application.dataPath + "/Resources/Levels/" + stageName.text + ".json");
            loadLevel = JsonConvert.DeserializeObject<Level>(str);

        }
        catch (FileNotFoundException e)
        {
            Debug.Log(e);
            return;
        }
        CellInstantiate(loadLevel);
        Debug.Log("Load complete.");
    }

    public void NewLevel()
    {
        CellInstantiate(emptyLevel);
        Debug.Log("Created a new Level");
    }

    public void resetLevel()
    {
        SceneManager.LoadScene("LevelEditor");
    }

    void CreateSaveDirectory()
    {
        string filePath = Application.dataPath + "/Resources";
        if (!Directory.Exists(filePath))
            AssetDatabase.CreateFolder("Assets", "Resources");

        filePath += "/Levels";
        if (!Directory.Exists(filePath))
            AssetDatabase.CreateFolder("Assets/Resources", "Levels");
        AssetDatabase.Refresh();
    }

    public void Save()
    {
        if(stageName.text == "")
        {
            Debug.Log("Enter a stage name!");
        }
        else
        {
            string filePath = Application.dataPath + "/Resources/Levels";

            if (!Directory.Exists(filePath))
                CreateSaveDirectory();

            string levelstr = JsonConvert.SerializeObject(newLevel);
            File.WriteAllText(Application.dataPath + "/Resources/Levels/" + stageName.text + ".json", levelstr);
            Debug.Log("Save complete.");
        }
        

    }
}
