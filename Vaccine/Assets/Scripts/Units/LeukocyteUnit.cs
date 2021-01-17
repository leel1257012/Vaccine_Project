using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeukocyteUnit : Unit
{
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private float range = 10.0f;
    [SerializeField]
    private float interval = 1.0f;
    private Vector3 shootPos;

    protected override void Start()
    {
        base.Start();
        MaxHealth = Health = 3;
        shootPos = gameObject.transform.position;
    }

    public override void GetDamaged(float damage)
    {

        if (!Invincible) base.GetDamaged(damage);

    }

    protected override Queue<IEnumerator> DecideNextRoutine()
    {
        Queue<IEnumerator> nextRoutines = new Queue<IEnumerator>();

        nextRoutines.Enqueue(NewActionRoutine(Fire(range, interval)));

        return nextRoutines;
    }

    private IEnumerator Fire(float range, float interval)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Row1");
        //GameObject temp;

        if (objects.Length != 0)
        {
            float min = range;
            foreach (GameObject element in objects)
            {
                float dist = Vector3.Distance(element.transform.position, GetObjectPos());
                if (dist < min)
                {
                    GameObject cur = Instantiate(bullet, shootPos, Quaternion.identity);
                    cur.GetComponent<Rigidbody2D>().velocity = -(GetObjectPos() - element.transform.position);
                    break;
                }
            }


        }

        yield return new WaitForSeconds(interval);
    }

}