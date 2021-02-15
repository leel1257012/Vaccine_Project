using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MozziPichi : Unit
{

    private float interval = 5.0f;
    private float knockBack = 5.0f;
    [SerializeField]
    private float scaleX = 2f;
    [SerializeField]
    private float scaleY = 1.7f;
    private float expandTime = 1.0f;
    private float ogX;
    private float ogY;

    public bool increase = false;

    protected override void Start()
    {
        base.Start();
        MaxHealth = Health = 9;
        ogX = gameObject.transform.localScale.x;
        ogY = gameObject.transform.localScale.y;
    }

    public override void GetDamaged(float damage)
    {

        if (!Invincible) base.GetDamaged(damage);

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
            nextRoutines.Enqueue(NewActionRoutine(IdleRoutine(2f)));
        }




        return nextRoutines;
    }

    private IEnumerator Expand(float interval)
    {
        float tempX = 0;
        float tempY = 0;

        increase = true;

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

        GameObject[] objects = GameObject.FindGameObjectsWithTag("Virus");

        if (objects.Length != 0)
        {
            float min = 3f;
            foreach (GameObject element in objects)
            {
                float dist = Vector3.Distance(element.transform.position, GetObjectPos());
                float yDist = Mathf.Abs(element.transform.position.y - gameObject.transform.position.y);
                if (dist < min && yDist <= 0.9f)
                {

                    //Debug.Log(dist);
                    element.transform.position += new Vector3(knockBack, 0, 0);
                }
            }

            yield return null;
        }
    }

    private IEnumerator Contract(float interval)
    {
        float tempX = 0;
        float tempY = 0;

        increase = false;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.attachedRigidbody.AddForce(10f * new Vector2(1,0));
    }

}