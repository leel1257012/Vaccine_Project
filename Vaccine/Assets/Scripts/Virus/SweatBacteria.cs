using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweatBacteria : VirusClass
{
    [SerializeField]
    private float damage = 1.0f;

    RaycastHit2D[] hits;
    float maxDistance = 0.0f;
    public Transform sweatRange;
    

    protected override void Start()
    {
        base.Start();
        MaxHealth = Health = 30;
        oriSpeed = 0.1f;
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
        target.GetComponent<Unit>().GetDamaged(damage);
        yield return new WaitForSeconds(0.5f);

        DetectSweat();

        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(0.5f);
    }

    private void DetectSweat()
    {
        maxDistance = Vector3.Distance(sweatRange.position, transform.position);
        hits = Physics2D.RaycastAll(transform.position, sweatRange.position - transform.position, maxDistance);

        Debug.DrawRay(transform.position, sweatRange.position - transform.position, Color.red, 0.3f);
        
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit2D hit = hits[i];
            if(hit.transform.tag == "Virus")
            {
                hit.transform.GetComponent<VirusClass>().ChangeSpeed(4.0f);
            }
        }
    }
}
