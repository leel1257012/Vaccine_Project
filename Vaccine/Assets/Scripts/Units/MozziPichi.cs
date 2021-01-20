using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MozziPichi : Unit
{

    private float interval = 3.0f;
    private float knockBack = 3.0f;
    [SerializeField]
    private float scaleX = 2f;
    [SerializeField]
    private float scaleY = 1.7f;
    private float expandTime = 1.0f;
    private float ogX;
    private float ogY;


    protected override void Start()
    {
        base.Start();
        MaxHealth = Health = 3;
        ogX = gameObject.transform.localScale.x;
        ogY = gameObject.transform.localScale.y;
    }

    public override void GetDamaged(float damage)
    {

        if (!Invincible) base.GetDamaged(damage);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<VirusClass>() != null)
        {
            //Debug.Log("1:" + collision.name);
            //Debug.Log("2:" + collision.gameObject.name);
            //collision.transform.Translate(new Vector3(3, 0, 0));
            collision.transform.position += new Vector3(3, 0, 0);
        }
    }

    protected override Queue<IEnumerator> DecideNextRoutine()
    {
        Queue<IEnumerator> nextRoutines = new Queue<IEnumerator>();

        if (gameObject.transform.localScale.x > 1.5f)
        {
            nextRoutines.Enqueue(NewActionRoutine(Contract(3f)));
            nextRoutines.Enqueue(NewActionRoutine(IdleRoutine(interval)));
        }

        else
        {
            nextRoutines.Enqueue(NewActionRoutine(Expand(3f)));
            nextRoutines.Enqueue(NewActionRoutine(IdleRoutine(interval)));
        }




        return nextRoutines;
    }

    private IEnumerator Expand(float interval)
    {
        float tempX = 0;
        float tempY = 0;

        for (float t = 0; t < 3f && gameObject.transform.localScale.x < scaleX; t += Time.deltaTime)
        {
            tempX = scaleX / (expandTime / 0.01f);
            tempY = scaleY / (expandTime / 0.01f);
            //Debug.Log(scaleX);
            //Debug.Log(expandTime);
            //Debug.Log(tempX);

            gameObject.transform.localScale += new Vector3(tempX, tempY, 0);
            yield return null;
        }
                
        yield return null;
    }

    private IEnumerator Contract(float interval)
    {
        float tempX = 0;
        float tempY = 0;

        for (float t = 0; t < 3f && gameObject.transform.localScale.x > ogX; t += Time.deltaTime)
        {
            tempX = scaleX / (expandTime / 0.01f);
            tempY = scaleY / (expandTime / 0.01f);
            //Debug.Log(scaleX);
            //Debug.Log(expandTime);
            //Debug.Log(tempX);

            gameObject.transform.localScale -= new Vector3(tempX, tempY, 0);
            yield return null;
        }

        yield return null;
    }

    private IEnumerator IdleRoutine(float time)
    {
        yield return new WaitForSeconds(time);
    }

}