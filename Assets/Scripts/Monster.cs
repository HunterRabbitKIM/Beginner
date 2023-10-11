using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    private NavMeshAgent nav;
    private Transform player;
    protected ViewAngle theViewAngle;
    protected Vector3 destination;  // 목적지

    private bool isAction;  // 행동 중인지 아닌지 판별
    private bool isWalking; // 걷는지, 안 걷는지 판별
    private bool isChasing;

    [SerializeField] private float walkTime;  // 걷기 시간
    [SerializeField] private float walkSpeed;  // 걷기 속력
    [SerializeField] private float waitTime;  // 대기 시간
    [SerializeField] private float chaseSpeed;  // 추격 속력
    [SerializeField] private float chaseTime;  // 최대 추격 시간
    [SerializeField] protected float currentChaseTime;
    [SerializeField] protected float chaseDelayTime; // 추격 딜레이

    private float currentTime;

    void Start()
    {
        isAction = true;   // 대기도 행동
        isChasing = false;
        currentTime = waitTime;
        nav = GetComponent<NavMeshAgent>();
        theViewAngle = GetComponent<ViewAngle>();
    }

    void Update()
    {
        Move();
        ElapseTime();
        if (theViewAngle.View())
        {
            StopAllCoroutines();
            StartCoroutine(ChaseTargetCoroutine());
        }
    }

    IEnumerator ChaseTargetCoroutine()
    {
        currentChaseTime = 0;
        Chase(theViewAngle.GetTargetPos());

        while (currentChaseTime < chaseTime)
        {
            Chase(theViewAngle.GetTargetPos());
            yield return new WaitForSeconds(chaseDelayTime);
            currentChaseTime += chaseDelayTime;
        }

        isChasing = false;
        nav.ResetPath();
    } 

    private void Move()
    {
        if (isWalking)
            nav.SetDestination(transform.position + destination * 5f);
    }

    private void ElapseTime()
    {
        if (isAction)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0 && !isChasing)  // 랜덤하게 다음 행동을 개시
                ReSet();
        }
    }

    protected virtual void ReSet()  // 다음 행동 준비
    {
        isAction = true;

        nav.ResetPath();

        isWalking = false;

        nav.speed = walkSpeed;

        destination.Set(Random.Range(-13f, 13f), 0f, Random.Range(-13f, 13f));

        RandomAction();
    }

    protected void TryWalk()  // 걷기
    {
        currentTime = walkTime;
        isWalking = true;
        nav.speed = walkSpeed;
        Debug.Log("걷기");
    }

    private void Wait()  // 대기
    {
        currentTime = waitTime;
        Debug.Log("대기");
    }


    private void RandomAction()
    {
        int _random = Random.Range(0, 2); // 대기, 걷기

        if (_random == 0)
            Wait();
        else if (_random == 1)
            TryWalk();
    }

        public void Chase(Vector3 _targetPos)
    {
        isChasing = true;

        destination = _targetPos;
        Debug.Log("추격중!");
        nav.speed = chaseSpeed;
        nav.SetDestination(destination);
    }
}
