using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    private NavMeshAgent nav;
    private Transform player;
    protected ViewAngle theViewAngle;
    protected Vector3 destination;  // ������

    private bool isAction;  // �ൿ ������ �ƴ��� �Ǻ�
    private bool isWalking; // �ȴ���, �� �ȴ��� �Ǻ�
    private bool isChasing;

    [SerializeField] private float walkTime;  // �ȱ� �ð�
    [SerializeField] private float walkSpeed;  // �ȱ� �ӷ�
    [SerializeField] private float waitTime;  // ��� �ð�
    [SerializeField] private float chaseSpeed;  // �߰� �ӷ�
    [SerializeField] private float chaseTime;  // �ִ� �߰� �ð�
    [SerializeField] protected float currentChaseTime;
    [SerializeField] protected float chaseDelayTime; // �߰� ������

    private float currentTime;

    void Start()
    {
        isAction = true;   // ��⵵ �ൿ
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
            if (currentTime <= 0 && !isChasing)  // �����ϰ� ���� �ൿ�� ����
                ReSet();
        }
    }

    protected virtual void ReSet()  // ���� �ൿ �غ�
    {
        isAction = true;

        nav.ResetPath();

        isWalking = false;

        nav.speed = walkSpeed;

        destination.Set(Random.Range(-13f, 13f), 0f, Random.Range(-13f, 13f));

        RandomAction();
    }

    protected void TryWalk()  // �ȱ�
    {
        currentTime = walkTime;
        isWalking = true;
        nav.speed = walkSpeed;
        Debug.Log("�ȱ�");
    }

    private void Wait()  // ���
    {
        currentTime = waitTime;
        Debug.Log("���");
    }


    private void RandomAction()
    {
        int _random = Random.Range(0, 2); // ���, �ȱ�

        if (_random == 0)
            Wait();
        else if (_random == 1)
            TryWalk();
    }

        public void Chase(Vector3 _targetPos)
    {
        isChasing = true;

        destination = _targetPos;
        Debug.Log("�߰���!");
        nav.speed = chaseSpeed;
        nav.SetDestination(destination);
    }
}
