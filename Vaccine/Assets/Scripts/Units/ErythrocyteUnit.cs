using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErythrocyteUnit : Unit
{
    float speed = 0.45f;
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
            yield return MoveRoutine(GetObjectPos() + new Vector3(speed, 0, 0), 0.1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Virus")
        {
            Destroy(gameObject);
            BuffSameRow();
        }
        
    }
    private void BuffSameRow()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Unit");

        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i].transform.position.y - gameObject.transform.position.y < 1)
            {
                objects[i].GetComponent<Unit>().ChangeAttackInterval(0.77f);
                Debug.Log(objects[i].name + " was buffed!");
            }
                

            
        }
    }

}
