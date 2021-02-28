using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefinitelyNotNeuron : Unit
{    
    [SerializeField]
    private float range = 9.5f;
    private int debuffCount = 0;

    protected override void Start()
    {
        base.Start();
        MaxHealth = Health = 9;
        OriAttackSpeed = 2.0f;
        attackSpeedInterval = OriAttackSpeed;
        Damage = 2.0f;
        Phrase = "다가오는 세균들을 향해 길게 돌기를 뻗어 세균들을 관통하여 공격한다.";
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
            nextRoutines.Enqueue(NewActionRoutine(Fire(range, 2 * attackSpeedInterval)));
        }
        else
        {
            debuffCount = 0;
            debuff = false;
            nextRoutines.Enqueue(NewActionRoutine(Fire(range, attackSpeedInterval)));
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
                gameObject.GetComponent<Transform>().localScale = new Vector3(0.288f, 0.144f, 0.144f);
                foreach (GameObject element in target)
                    element.GetComponent<VirusClass>().GetDamaged(Damage);
                yield return new WaitForSeconds(0.5f);
                gameObject.GetComponent<Transform>().localScale = new Vector3(0.144f, 0.144f, 0.144f);
            }
        }

        yield return new WaitForSeconds(interval);
    }
}
