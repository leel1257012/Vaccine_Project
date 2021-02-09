using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthyWallUnit : Unit
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        MaxHealth = Health = 5;
    }

    public override void GetDamaged(float damage)
    {

        if (!Invincible) base.GetDamaged(damage);

    }
    protected override Queue<IEnumerator> DecideNextRoutine()
    {
        Queue<IEnumerator> nextRoutines = new Queue<IEnumerator>();

        //nextRoutines.Enqueue(NewActionRoutine(Fire(range, interval)));

        return nextRoutines;
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
