using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyUnit : Unit
{
    // (temp) Place it in row3
    protected override void Start()
    {
        base.Start();
        MaxHealth = Health = 1;
    }

    public override void GetDamaged(float damage)
    {

        if (!Invincible) base.GetDamaged(damage);

    }


    protected override Queue<IEnumerator> DecideNextRoutine()
    {
        Queue<IEnumerator> nextRoutines = new Queue<IEnumerator>();

        nextRoutines.Enqueue(NewActionRoutine(IdleRoutine(10f)));

        return nextRoutines;
    }

    private IEnumerator IdleRoutine(float time)
    {
        yield return new WaitForSeconds(time);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<VirusClass>() != null)
        {
            gameObject.transform.localScale += new Vector3(0f, 5f, 0f);
        }
    }

}