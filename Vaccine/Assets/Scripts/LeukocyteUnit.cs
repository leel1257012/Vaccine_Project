using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeukocyteUnit : Unit
{

    bool Invincible = false;

    protected override void Start()
    {
        base.Start();
        MaxHealth = Health = 350;
    }

    public override void GetDamaged(float damage)
    {

        if (!Invincible) base.GetDamaged(damage);
        if (Health <= 0)
            gameObject.SetActive(false);
    }

    protected override Queue<IEnumerator> DecideNextRoutine()
    {
        Queue<IEnumerator> nextRoutines = new Queue<IEnumerator>();

        return nextRoutines;
    }
}
