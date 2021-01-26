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
    private float attackInterval = 1.0f;
    private int debuffCount = 0;
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

        if (debuff && debuffCount < 2)
        {
            debuffCount++;
            nextRoutines.Enqueue(NewActionRoutine(Fire(range, attackInterval + 1)));
        }
        else
        {
            debuffCount = 0;
            debuff = false;
            nextRoutines.Enqueue(NewActionRoutine(Fire(range, attackInterval)));

        }

        return nextRoutines;
    }

    private IEnumerator Fire(float range, float interval)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Virus");
        GameObject temp = null;
        bool find = false;

        if (objects.Length != 0)
        {
            float min = range;
            foreach (GameObject element in objects)
            {
                float dist = Vector3.Distance(element.transform.position, GetObjectPos());
                float yDist = Mathf.Abs(element.transform.position.y - GetObjectPos().y);
                if (dist < min && yDist <= 1)
                {
                    temp = element;
                    min = dist;
                    find = true;
                }
            }
            if (find)
            {
                GameObject cur = Instantiate(bullet, shootPos, Quaternion.identity);
                cur.GetComponent<Rigidbody2D>().velocity = (temp.transform.position - GetObjectPos());
            }


        }

        yield return new WaitForSeconds(interval);
    }

}