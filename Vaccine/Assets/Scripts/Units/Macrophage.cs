using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Macrophage : Unit
{
    private bool collided = false;
    private bool digesting = false;
    private Collider2D target;
    [SerializeField]
    private GameObject Erythrocyte;
    private Vector3 targetPos;

    protected override void Start()
    {
        base.Start();
        MaxHealth = Health = 9;
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

        if (collided && !digesting)
            nextRoutines.Enqueue(NewActionRoutine(Digest()));
        else
            nextRoutines.Enqueue(NewActionRoutine(Wait()));

        return nextRoutines;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Transform>().tag == "Virus")
        {
            collided = true;
            target = collision;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Transform>().tag == "Virus")
        {
            collided = false;
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.0f);
    }

    private IEnumerator Digest()
    {
        targetPos = target.transform.position;
        Destroy(target.gameObject);

        digesting = true;
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(5.0f);

        Instantiate(Erythrocyte, targetPos, Quaternion.identity);
        digesting = false;
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

}