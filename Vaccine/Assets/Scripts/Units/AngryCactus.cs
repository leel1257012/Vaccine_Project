using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AngryCactus : Unit
{
    [SerializeField]
    private float damage = 1.0f;
    [SerializeField]
    private float attackInterval = 0.5f;
    private List<GameObject> target = new List<GameObject>();

    protected override void Start()
    {
        base.Start();
        MaxHealth = Health = 999;
    }

    public override void GetDamaged(float damage)
    {

        if (!Invincible) base.GetDamaged(damage);

    }

    protected override Queue<IEnumerator> DecideNextRoutine()
    {
        Queue<IEnumerator> nextRoutines = new Queue<IEnumerator>();

        nextRoutines.Enqueue(NewActionRoutine(Attack(attackInterval)));

        return nextRoutines;
    }

    private IEnumerator Attack(float interval)
    {
        GameObject t;
        for(int i = target.Count - 1; i >= 0 ; i--)
        {
            t = target[i];
            t.GetComponent<VirusClass>().GetDamaged(damage);
        }

        yield return new WaitForSeconds(interval);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<VirusClass>() != null)
        {
            target.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<VirusClass>() != null)
        {
            target.Remove(collision.gameObject);
        }
    }
}
