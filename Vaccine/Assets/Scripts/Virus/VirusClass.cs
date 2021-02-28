using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public abstract class VirusClass : MonoBehaviour
{
    protected Coroutine CurrentRoutine { get; private set; }
    private Queue<IEnumerator> nextRoutines = new Queue<IEnumerator>();

    public float Health { get; protected set; }
    public float MaxHealth { get; protected set; }
    public float OriAttackSpeed { get; protected set; }
    public float Damage { get; protected set; }
    public float speed { get; protected set; }
    public float oriSpeed;
    int speedBuffed = 0;

    protected bool collided = false;

    public bool Invincible = false;

    protected Animator animator;

    protected Collider2D col;
    protected Rigidbody2D rb;

    protected Collider2D target;

    [SerializeField]
    private GameObject status;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        status = GameObject.Find("Canvas").transform.Find("Status").gameObject;
    }

    private void Update()
    {
        if (CurrentRoutine == null)
        {
            NextRoutine();
        }
    }

    public virtual void GetDamaged(float damage)
    {
        Health -= damage;
        if (Health <= 0) Destroy(gameObject);

    }

    protected Vector3 GetObjectPos()
    {
        return gameObject.transform.position;
    }


    private void NextRoutine()
    {
        if (nextRoutines.Count <= 0)
        {
            nextRoutines = DecideNextRoutine();
        }
        StartCoroutineUnit(nextRoutines.Dequeue());
    }

    protected abstract Queue<IEnumerator> DecideNextRoutine();

    private void StartCoroutineUnit(IEnumerator coroutine)
    {
        if (CurrentRoutine != null)
        {
            StopCoroutine(CurrentRoutine);
        }
        CurrentRoutine = StartCoroutine(coroutine);
    }

    protected IEnumerator NewActionRoutine(IEnumerator action)
    {
        yield return action;
        CurrentRoutine = null;
    }

    protected IEnumerator MoveRoutine(Vector3 destination, float time)
    {
        yield return MoveRoutine(destination, time, AnimationCurve.Linear(0, 0, 1, 1));
    }
    protected IEnumerator MoveRoutine(Vector3 destination, float time, AnimationCurve curve)
    {
        Vector3 startPosition = transform.position;
        //Debug.Log(startPosition);
        for (float t = 0; t <= time; t += Time.deltaTime)
        {
            transform.position =
                Vector3.Lerp(startPosition, destination, curve.Evaluate(t / time));
            yield return null;
        }
        transform.position = destination;
    }

    protected IEnumerator WaitRoutine(float time)
    {
        yield return new WaitForSeconds(time);
    }

    //protected void OnCollisionEnter2D(Collision2D collision)
    //{
    //    collided = true;
        
    //}

    //protected void OnCollisionExit2D(Collision2D collision)
    //{
    //    collided = false;
        
    //}

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Unit>() != null && !(collision.GetComponent<AngryCactus>() != null)
            && !(collision.GetComponent<StickyUnit>() != null)
            && !(collision.GetComponent<ErythrocyteUnit>() != null))
        {
            collided = true; 
            target = collision;
        }
        //if(collision.gameObject.GetComponent<MozziPichi>() != null
        //    && collision.gameObject.)
        //{
        //    Debug.Log("Hit");
        //}

    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Unit>() != null && !(collision.GetComponent<AngryCactus>() != null)
            && !(collision.GetComponent<StickyUnit>() != null)
            && !(collision.GetComponent<ErythrocyteUnit>() != null)) collided = false;

    }

    public void ChangeSpeed(float multiple)
    {
        speed = oriSpeed * multiple;
        speedBuffed++;
        Invoke("RestoreSpeed", 0.5f);
    }

    void RestoreSpeed()
    {
        speedBuffed--;
        if (speedBuffed == 0)
            speed = oriSpeed;
    }

    private void OnMouseEnter()
    {
        status.SetActive(true);
        status.GetComponent<Transform>().position = Input.mousePosition;

        status.GetComponent<Transform>().Find("Image").GetComponent<Image>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        status.GetComponent<Transform>().Find("Damage").GetComponent<TextMeshProUGUI>().text = "Damage: " + Damage;
        status.GetComponent<Transform>().Find("Health").GetComponent<TextMeshProUGUI>().text = "Health: " + MaxHealth;
        status.GetComponent<Transform>().Find("AttackSpeed").GetComponent<TextMeshProUGUI>().text = "AttackSpeed: " + Math.Round((1 / OriAttackSpeed), 2);
    }
    private void OnMouseExit()
    {
        //status.SetActive(false);
    }
}

