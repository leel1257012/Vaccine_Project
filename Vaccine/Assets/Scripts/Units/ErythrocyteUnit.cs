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
                //Debug.Log(objects[i].transform.position.y - gameObject.transform.position.y);
                gameObject.GetComponent<Unit>().ChangeAttackInterval(0.7f);
                Debug.Log(objects[i].name + " was buffed!");
            }
                

            
        }
    }

}
