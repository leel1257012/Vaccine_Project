using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoughBacteria : VirusClass
{
    [SerializeField]
    private float damage = 1.0f;

    protected override void Start()
    {
        base.Start();
        MaxHealth = Health = 30;
    }

    protected override Queue<IEnumerator> DecideNextRoutine()
    {
        Queue<IEnumerator> nextRoutines = new Queue<IEnumerator>();

        if (!collided) nextRoutines.Enqueue(NewActionRoutine(Move()));
        else
        {
            nextRoutines.Enqueue(NewActionRoutine(Cough()));
        }

        return nextRoutines;
    }

    private IEnumerator Move()
    {
        yield return MoveRoutine(GetObjectPos() + new Vector3(-0.1f, 0, 0), 0.1f);

    }

    private IEnumerator Cough()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        target.GetComponent<Unit>().GetDamaged(damage);
        yield return new WaitForSeconds(0.25f);

        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(0.25f);
    }
}
