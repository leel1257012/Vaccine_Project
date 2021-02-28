using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweatBacteria : VirusClass
{
    RaycastHit2D[] hits;
    float maxDistance = 0.0f;

    protected override void Start()
    {
        base.Start();
        MaxHealth = Health = 30;
        OriAttackSpeed = 1.0f;
        Damage = 1.0f;
        Phrase = "세포를 향해 다가가며 땀을 흘린다. 땀 위에 있는 세균들은 이동속도가 빨라지며 세포들은 데미지를 입는다.";
        oriSpeed = 0.09375f;
        speed = oriSpeed;
    }

    public override void GetDamaged(float damage)
    {

        if (!Invincible) base.GetDamaged(damage);

    }
    protected override Queue<IEnumerator> DecideNextRoutine()
    {
        Queue<IEnumerator> nextRoutines = new Queue<IEnumerator>();

        if (!collided) nextRoutines.Enqueue(NewActionRoutine(MoveSweating()));
        else
        {
            nextRoutines.Enqueue(NewActionRoutine(JustSweating()));
        }

        return nextRoutines;
    }

    private IEnumerator MoveSweating()
    {
        DetectSweat();
        
        yield return MoveRoutine(GetObjectPos() + new Vector3(-speed, 0, 0), 0.1f);
    }

    private IEnumerator JustSweating()
    {
        DetectSweat();

        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        target.GetComponent<Unit>().GetDamaged(Damage);
        yield return new WaitForSeconds(0.5f);

        DetectSweat();

        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(0.5f);
    }

    private void DetectSweat()
    {
        maxDistance = Vector3.Distance(gameObject.transform.GetChild(1).GetComponent<SweatRange>().myPos, transform.position);
        //maxDistance = Vector3.Distance(sweatRange.position, transform.position);
        hits = Physics2D.RaycastAll(transform.position, gameObject.transform.GetChild(1).GetComponent<SweatRange>().myPos - transform.position, maxDistance);
        //hits = Physics2D.RaycastAll(transform.position, sweatRange.position - transform.position, maxDistance);

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit2D hit = hits[i];
            if(hit.transform.tag == "Virus" && !(hit.collider.gameObject == gameObject))
            {
                hit.transform.GetComponent<VirusClass>().ChangeSpeed(1.3f);
            }
        }
    }
}
