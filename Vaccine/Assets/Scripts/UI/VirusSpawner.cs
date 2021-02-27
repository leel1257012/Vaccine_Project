using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusSpawner : MonoBehaviour
{
    public List<GameObject> virusPrefabs;
    public List<GameObject> spawnPoints;
    //public List<Virus> viruses;
    public GameObject spawner;
    private GameManager gameManager;
    private int i, j;
    private int[,] arr;
    private float time;

    private void Start()
    {
        gameManager = GameManager.instance;
        arr = gameManager.array;
        i = arr.GetLength(0);
        j = arr.GetLength(1);
        time = 1f;
        StartCoroutine(Spawn());

        
    }

    IEnumerator Spawn()
    {
        for (int x = 0; x < j; x++)
        {
            for (int y = 0; y < i; y++)
            {
                if (arr[y, x] != 0 && arr[y,x] != -1)
                {
                    Instantiate(virusPrefabs[arr[y, x]], spawnPoints[y].transform.position, Quaternion.identity);
                    //Debug.Log("virus: " + arr[y, x] + '\n');
                    //Debug.Log("spawner: " + spawnPoints[y].transform.position + '\n');
                    yield return new WaitForSeconds(0f);
                }
                else yield return new WaitForSeconds(0f);
            }
            yield return new WaitForSeconds(2.5f);
        }
    }
    //IEnumerator Wait()
    //{
    //    yield return new WaitForSeconds(2f);
    //}
}



