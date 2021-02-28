using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fever : VirusClass
{

    private float damage = 7f;
    private float interval = 0.25f;

    protected override void Start()
    {
        base.Start();
        Damage = 7.0f;
        OriAttackSpeed = 0f;
        oriSpeed = 0.15f;
        speed = oriSpeed;
        MaxHealth = Health = 15;
        Phrase = "세포에게 빠르게 다가가 자신의 몸을 뜨겁게 만들어 자폭한다.";
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
            nextRoutines.Enqueue(NewActionRoutine(SelfDestruct()));
        }

        return nextRoutines;
    }

    private IEnumerator SelfDestruct()
    {
        animator.SetTrigger("Destruct");
        for (float t = 0; t < interval; t += Time.deltaTime) yield return null;
        if(target != null)
        {
            if (target.GetComponent<Unit>() != null)
            {
                target.GetComponent<Unit>().GetDamaged(damage);

            }
        }
        
        gameObject.GetComponent<VirusClass>().GetDamaged(MaxHealth);


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
        //        for (float t = 0; t < interval; t += Time.deltaTime) yield return null;
        //        animator.SetTrigger("Destruct");
        //        temp.GetComponent<Unit>().GetDamaged(damage);
        //        gameObject.GetComponent<VirusClass>().GetDamaged(MaxHealth);
        //    }

        //}

        yield return null;

    }

    private IEnumerator Move()
    {
        yield return MoveRoutine(GetObjectPos() + new Vector3(-speed, 0, 0), 0.05f);

    }

    private IEnumerator IdleRoutine(float time)
    {
        yield return new WaitForSeconds(time);
    }

}