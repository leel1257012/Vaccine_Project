using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snot : VirusClass
{

    private float interval = 1.0f;
    private float damage = 1.0f;

    protected override void Start()
    {
        base.Start();
        oriSpeed = 0.1f;
        speed = oriSpeed;
        MaxHealth = Health = 15;
    }

    public override void GetDamaged(float damage)
    {

        if (!Invincible) base.GetDamaged(damage);

    }

    protected override Queue<IEnumerator> DecideNextRoutine()
    {
        Queue<IEnumerator> nextRoutines = new Queue<IEnumerator>();

        if (!collided) nextRoutines.Enqueue(NewActionRoutine(Move()));
        else
        {
            nextRoutines.Enqueue(NewActionRoutine(MeleeAttack(interval)));
        }

        return nextRoutines;
    }

    private IEnumerator MeleeAttack(float interval)
    {
        animator.SetTrigger("Attack");
        target.GetComponent<Unit>().GetDamaged(damage);
        target.GetComponent<Unit>().ChangeAttackInterval(2f);
        yield return new WaitForSeconds(interval);

        //GameObject[] objects = GameObject.FindGameObjectsWithTag("Unit");
        //GameObject temp = null;
        //bool find = false;

        //if (objects.Length != 0)
        //{
        //    float min = 999f;
        //    foreach (GameObject element in objects)
        //    {
        //        float dist = Vector3.Distance(element.transform.position, GetObjectPos());
        //        float yDist = Mathf.Abs(element.transform.position.y - GetObjectPos().y);
        //        if (dist < min && yDist <= 1)
        //        {
        //            temp = element;
        //            min = dist;
        //            find = true;
        //        }
        //    }
        //    if (find)
        //    {
        //        animator.SetTrigger("Attack");
        //        temp.GetComponent<Unit>().GetDamaged(damage);
        //        temp.GetComponent<Unit>().debuff = true;
        //        yield return new WaitForSeconds(interval);
        //    }

        //}

        yield return null;

    }

    private IEnumerator Move()
    {
        yield return MoveRoutine(GetObjectPos() + new Vector3(-speed, 0, 0), 0.1f);

    }

    private IEnumerator IdleRoutine(float time)
    {
        yield return new WaitForSeconds(time);
    }

}