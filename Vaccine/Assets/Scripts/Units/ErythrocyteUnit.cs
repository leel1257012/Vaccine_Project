using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErythrocyteUnit : Unit
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected override Queue<IEnumerator> DecideNextRoutine()
    {
        Queue<IEnumerator> nextRoutines = new Queue<IEnumerator>();

        nextRoutines.Enqueue(NewActionRoutine(Move()));

        return nextRoutines;
    }

    private IEnumerator Move()
    {
        if (gameManager.start)
            yield return MoveRoutine(GetObjectPos() + new Vector3(3, 0, 0), 10f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        BuffSameRow();
    }
    private void BuffSameRow()
    {
        string row = gameObject.tag;
        GameObject[] objects = GameObject.FindGameObjectsWithTag(row);

        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i].name == gameObject.name)
                continue;

            Debug.Log(objects[i].name + " was buffed!");
        }
    }

}
