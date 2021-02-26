using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthyWallUnit : Unit
{
    protected override void Start()
    {
        base.Start();
        MaxHealth = Health = 20;
        OriAttackSpeed = 0;
        Damage = 0;
    }

    public override void GetDamaged(float damage)
    {

        if (!Invincible) base.GetDamaged(damage);

    }
    protected override Queue<IEnumerator> DecideNextRoutine()
    {
        Queue<IEnumerator> nextRoutines = new Queue<IEnumerator>();

        return nextRoutines;
    }

    private void Update()
    {
        
    }
}
