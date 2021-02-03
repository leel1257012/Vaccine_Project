using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefinitelyNotNeuron : Unit
{    
    [SerializeField]
    private float range = 10.0f;
    [SerializeField]
    private float attackInterval = 2.0f;
    [SerializeField]
    private float damage = 2.0f;
    private int debuffCount = 0;

    protected override void Start()
    {
        base.Start();
        MaxHealth = Health = 9;
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
            nextRoutines.Enqueue(NewActionRoutine(Fire(range, 2 * attackInterval)));
        }
        else
        {
            debuffCount = 0;
            debuff = false;
            nextRoutines.Enqueue(NewActionRoutine(Fire(range, attackInterval)));
        }

        return nextRoutines;
    }

    private IEnumerator Fire(float range, float interval)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Virus");
        List<GameObject> target = new List<GameObject>();
        bool find = false;

        if (objects.Length != 0)
        {
            foreach (GameObject element in objects)
            {
                float dist = Vector3.Distance(element.transform.position, GetObjectPos());
                float yDist = Mathf.Abs(element.transform.position.y - GetObjectPos().y);
                if(dist < range && yDist <= 1)
                {
                    find = true;
                    target.Add(element);
                }
            }
            if (find)
            {
                gameObject.GetComponent<Transform>().localScale = new Vector3(0.7425f, 0.37125f, 0.37125f);
                foreach (GameObject element in target)
                    element.GetComponent<VirusClass>().GetDamaged(damage);
                yield return new WaitForSeconds(0.5f);
                gameObject.GetComponent<Transform>().localScale = new Vector3(0.37125f, 0.37125f, 0.37125f);
            }
        }

        yield return new WaitForSeconds(interval);
    }
}
