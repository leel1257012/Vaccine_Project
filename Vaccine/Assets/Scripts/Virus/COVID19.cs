using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COVID19 : VirusClass
{

    protected override void Start()
    {
        base.Start();
        MaxHealth = Health = 30;
    }

    public override void GetDamaged(float damage)
    {

        if (!Invincible) base.GetDamaged(damage);

    }

    protected override Queue<IEnumerator> DecideNextRoutine()
    {
        Queue<IEnumerator> nextRoutines = new Queue<IEnumerator>();

        nextRoutines.Enqueue(NewActionRoutine(Move()));

        return nextRoutines;
    }

    private IEnumerator Move()
    {
        yield return MoveRoutine(GetObjectPos() + new Vector3(-1, 0, 0), 1f);
    }

}