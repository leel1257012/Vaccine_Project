using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidLeukocyteUnit : Unit
{
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private float range = 10.0f;
    private Vector3 shootPos;
    private float attackInterval = 1.0f;
    private int debuffCount = 0;

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

        if (debuff && debuffCount < 4)
        {
            nextRoutines.Enqueue(NewActionRoutine(FireAny(range, attackInterval + 1)));
        }
        else
        {
            debuffCount = 0;
            debuff = false;
            nextRoutines.Enqueue(NewActionRoutine(FireAny(range, attackInterval)));

        }

        

        return nextRoutines;
    }

    private IEnumerator FireAny(float range, float interval)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Virus");
        GameObject temp = null;

        if (objects.Length != 0)
        {
            float min = range;
            foreach (GameObject element in objects)
            {
                float dist = Vector3.Distance(element.transform.position, GetObjectPos());
                if (dist < min)
                {
                    temp = element;
                    min = dist;
                }
            }

            if (temp != null)
            {
                GameObject cur = Instantiate(bullet, shootPos, Quaternion.identity);
                cur.GetComponent<Rigidbody2D>().velocity = (temp.transform.position - GetObjectPos());
            }


        }

        yield return new WaitForSeconds(interval);
    }
}
