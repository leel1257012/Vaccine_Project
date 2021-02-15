using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweatRange : MonoBehaviour
{
    private Vector3 followPos;
    private int followDelay = 6; // 땀 자국이 없어지기까지의 시간
    private float passedTime = 0; // 게임 시작 이후 지난 시간
    private Queue<Vector3> parentPos;

    public Vector3 myPos;

    private void Awake()
    {
        followPos = gameObject.transform.parent.position;
        parentPos = new Queue<Vector3>();
    }
        
    void Update()
    {
        passedTime += Time.deltaTime;
        Watch();
        Follow();
    }

    void Watch()
    {
        parentPos.Enqueue(gameObject.transform.parent.position);

        if(passedTime > followDelay)
            followPos = parentPos.Dequeue();
    }

    void Follow()
    {
        myPos = followPos;
    }
}
