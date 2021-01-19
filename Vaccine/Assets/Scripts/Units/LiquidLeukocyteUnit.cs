using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidLeukocyteUnit : Unit
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

        nextRoutines.Enqueue(NewActionRoutine(FireAny(range, interval)));

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

            GameObject cur = Instantiate(bullet, shootPos, Quaternion.identity);
            cur.GetComponent<Rigidbody2D>().velocity = (temp.transform.position - GetObjectPos());


        }

        yield return new WaitForSeconds(interval);
    }
}
