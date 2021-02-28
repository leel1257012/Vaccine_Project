using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyUnit : Unit
{

    private GameObject cur;
    private bool colliding = false;

    protected override void Start()
    {
        base.Start();
        Damage = 0f;
        cur = new GameObject();
        cur.transform.position = gameObject.transform.position;
        cur.transform.localScale = gameObject.transform.localScale;
        MaxHealth = Health = 999;
    }

    private void FixedUpdate()
    {
        colliding = false;
    }

    private void Update()
    {
        Debug.Log(cur.transform.position);
        if (!colliding)
        {
            gameObject.transform.position = cur.transform.position;
            gameObject.transform.localScale = cur.transform.localScale;
        } 
    }

    public override void GetDamaged(float damage)
    {

        if (!Invincible) base.GetDamaged(damage);

    }


    protected override Queue<IEnumerator> DecideNextRoutine()
    {
        Queue<IEnumerator> nextRoutines = new Queue<IEnumerator>();

        nextRoutines.Enqueue(NewActionRoutine(IdleRoutine(10f)));

        return nextRoutines;
    }

    private IEnumerator IdleRoutine(float time)
    {
        yield return new WaitForSeconds(time);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<VirusClass>() != null)
        {
            colliding = true;
            gameObject.transform.position = new Vector3(cur.transform.position.x, -0.43f, 0);
            gameObject.transform.localScale = new Vector3(1f, 6f, 1f);
            collision.GetComponent<VirusClass>().ChangeSpeed(0.5f);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<VirusClass>() != null)
        {
            colliding = true;
            collision.GetComponent<VirusClass>().ChangeSpeed(0.5f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<VirusClass>() != null)
        {
            colliding = false;
        }
    }

}