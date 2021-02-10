using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public abstract class Unit : MonoBehaviour
{
    protected GameManager gameManager;

    protected Coroutine CurrentRoutine { get; private set; }
    private Queue<IEnumerator> nextRoutines = new Queue<IEnumerator>();

    public float Health { get; protected set; }
    public float MaxHealth { get; protected set; }
    public float OriAttackSpeed { get; protected set; }
    public float attackSpeedInterval { get; protected set; }
    private int attackSpeedBuff = 0;

    public bool Invincible = false;

    public bool debuff = false;

    protected Animator animator;

    protected Collider2D col;
    protected Rigidbody2D rb;

    [SerializeField]
    private GameObject status;

    protected virtual void Start()
    {
        gameManager = GameManager.instance;
        animator = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        status = GameObject.Find("Canvas").transform.Find("Status").gameObject;
    }

    private void Update()
    {
        if(CurrentRoutine == null && gameManager.start)
        {
            NextRoutine();
        }
    }

    public virtual void GetDamaged(float damage)
    {
        Health -= damage;
        Debug.Log(gameObject.name + ":" + Health);
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

    public void ChangeAttackInterval(float multiple)
    {
        attackSpeedInterval = OriAttackSpeed * multiple;
        attackSpeedBuff++;
        Invoke("RestoreAttackSpeed", 3f);
    }

    private void RestoreAttackSpeed()
    {
        attackSpeedBuff--;
        if (attackSpeedBuff == 0) attackSpeedInterval = OriAttackSpeed;
    }

    private void OnMouseEnter()
    {
        status.SetActive(true);
        status.GetComponent<Transform>().position = Input.mousePosition;

        //status.GetComponent<Transform>().Find("Damage").GetComponent<TextMeshProUGUI>().text = "Damage: " + damage;
        status.GetComponent<Transform>().Find("Health").GetComponent<TextMeshProUGUI>().text = "Health: " + MaxHealth;
        status.GetComponent<Transform>().Find("AttackSpeed").GetComponent<TextMeshProUGUI>().text = "AttackSpeed: " + Math.Round((1 / OriAttackSpeed), 2);
    }
    private void OnMouseExit()
    {
        status.SetActive(false);
    }
}
