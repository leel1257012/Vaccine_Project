using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryCactus : Unit
{
    [SerializeField]
    private float damage = 1.0f;
    [SerializeField]
    private float attackInterval = 0.5f;
    private int debuffCount = 0;

    protected override void Start()
    {
        base.Start();
        MaxHealth = Health = 3;
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

    private IEnumerator Fire()
    {
        yield return new WaitForSeconds(0f);
    }
}
