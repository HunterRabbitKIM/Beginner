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
    private Animator Animator;

    private bool isAction;  // �ൿ ������ �ƴ��� �Ǻ�
    private bool isWalking; // �ȴ���, �� �ȴ��� �Ǻ�
    private bool isChasing;
    private bool isWaiting = true;


    [SerializeField] private float walkTime;  // �ȱ� �ð�
    [SerializeField] private float walkSpeed;  // �ȱ� �ӷ�
    [SerializeField] private float waitTime;  // ��� �ð�
    [SerializeField] private float chaseSpeed;  // �߰� �ӷ�
    [SerializeField] private float chaseTime;  // �ִ� �߰� �ð�
    [SerializeField] protected float currentChaseTime;
    [SerializeField] protected float chaseDelayTime; // �߰� ������

    private float currentTime;
    private static readonly int HashIswalking = Animator.StringToHash("IsWalking");
    private static readonly int HashIsChasing = Animator.StringToHash("IsChasing");

    void Start()
    {
        isAction = true;   // ��⵵ �ൿ
        isChasing = false;
        currentTime = waitTime;
        nav = GetComponent<NavMeshAgent>();
        theViewAngle = GetComponent<ViewAngle>();
        Animator = GetComponent<Animator>();
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
        Animator.SetBool(HashIsChasing,isChasing);
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

        isWalking = !isWaiting;  // ��� ���Ŀ��� ������ �ȱ�

        isWaiting = !isWaiting;  // �ȱ� ���Ŀ��� ������ ���

        nav.speed = walkSpeed;

        destination.Set(Random.Range(-12f, 12f), 0f, Random.Range(-12.25f, 12.25f));

        RandomAction();
        
        Animator.SetBool(HashIswalking,isWalking);
        Animator.SetBool(HashIswalking,isAction);
    }

    protected void TryWalk()  // �ȱ�
    {
        currentTime = walkTime;
        isWalking = true;
        nav.speed = walkSpeed;
        Debug.Log("�ȱ�");
        Animator.SetBool(HashIswalking,isWalking);
    }

    private void Wait()  // ���
    {
        currentTime = waitTime;
        Debug.Log("���");
    }

    private void RandomAction()
    {
        if (isWaiting)
            Wait();
        else
            TryWalk();
    }

        public void Chase(Vector3 _targetPos)
    {
        isChasing = true;

        destination = _targetPos;
        Debug.Log("�߰���!");
        nav.speed = chaseSpeed;
        nav.SetDestination(destination);
        Animator.SetBool(HashIsChasing,isChasing);
    }
}
