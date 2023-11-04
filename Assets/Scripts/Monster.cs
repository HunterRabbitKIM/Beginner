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
    [SerializeField] protected float chaseDelayTime;

    public float raycastDistance = 5.0f;  // ������ ����
    public LayerMask wallMask;  // �� ���̾� ����ũ

    private float currentTime;
    private static readonly int HashIswalking = Animator.StringToHash("IsWalking");
    private static readonly int HashIsChasing = Animator.StringToHash("IsChasing");

    private HashSet<Vector3> visitedLocations = new HashSet<Vector3>(); // �̹� �湮�� ��ġ ����


    void Start()
    {
        currentTime = waitTime;
        isAction = true;   // ��⵵ �ൿ
        isChasing = false;
        nav = GetComponent<NavMeshAgent>();
        theViewAngle = GetComponent<ViewAngle>();
        Animator = GetComponent < Animator>();

        SetNewRandomDestination();
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
        Animator.SetBool(HashIsChasing, isChasing);
    }

    private void Move()
    {
        if (isWalking)
        {
            if (IsInMazeArea(transform.position))
            {
                // �̷� ���� ���� �ִٸ� �ٸ� ��ǥ ������ ����
                SetNewRandomDestination();
            }
            else
            {
            // ���� �����ϰ� ȸ�� ��� ���
                if (DetectWall())
                {
                    CalculateAvoidancePath();
                }
                else
                {
                    // NavMesh Agent�� ����Ͽ� ��ǥ �������� �̵�
                    nav.SetDestination(destination);

                    // �������� ��ȿ�� ��� �湮�� ��ġ�� �߰�
                    visitedLocations.Add(destination);
                }

                // NavMesh Agent�� ��ǥ ������ �������� �� ���ο� ��ǥ ���� ����
                if (!nav.pathPending && nav.remainingDistance < 0.1f)
                {
                    SetNewRandomDestination();
                }
            }

        }

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

    protected virtual void ReSet()
    {
        isAction = true;
        isWalking = false;
        Animator.SetBool(HashIswalking, isWalking);
        isWaiting = false;
        Animator.SetBool(HashIswalking, isAction);
        nav.ResetPath();
        nav.speed = walkSpeed;
        SetNewRandomDestination();
        if (isWaiting)
            Wait();
        else
            TryWalk();
    }

    private void SetNewRandomDestination()
    {
        Vector3 newDestination;
        do
        {
            // ���ο� ���� ��ǥ ���� ����
            newDestination = new Vector3(Random.Range(-110f, 110f), 0f, Random.Range(-110f, 110f));
        } while (visitedLocations.Contains(newDestination)); // �̹� �湮�� ��ġ��� �ٽ� ����

        destination = newDestination;

        // �ִ� ��� ���
        if (nav.CalculatePath(destination, new NavMeshPath()))
        {
            isWalking = true;
            Animator.SetBool(HashIswalking, isWalking);
        }
        else
        {
            // ��ǥ ������ ���� �Ұ����� ��� �ٽ� ����
            SetNewRandomDestination();
        }
    }

    protected void TryWalk()
    {
        currentTime = walkTime;
        isWalking = true;
        Animator.SetBool(HashIswalking, isWalking);
        nav.speed = walkSpeed;
        //Debug.Log("�ȱ�");
    }

    private void Wait()
    {
        currentTime = waitTime;
        Animator.SetBool(HashIswalking, false);
        nav.ResetPath();
        //Debug.Log("���");
    }

    public void Chase(Vector3 _targetPos)
    {
        isChasing = true;
        destination = _targetPos;
        nav.SetDestination(destination);
        Animator.SetBool(HashIsChasing, isChasing);
        nav.speed = chaseSpeed;
        //Debug.Log("�߰���!");
    }
    private bool DetectWall()
    {
        // Raycast�� ����Ͽ� ���� ����
        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 wallDirection = Quaternion.Euler(0, 45, 0) * forward; // 45�� ȸ���� ����

        if (Physics.Raycast(transform.position, wallDirection, out hit, raycastDistance, wallMask))
        {
            // ���� ������ ���
            Debug.DrawRay(transform.position, wallDirection * raycastDistance, Color.red);
            return true;
        }
        else
        {
            // ���� �������� ���� ���
            Debug.DrawRay(transform.position, wallDirection * raycastDistance, Color.green);
            return false;
        }
    }
    private void CalculateAvoidancePath()
    {
        // ���� ��ġ�� ���� ��ǥ ���� ���� �߰� ������ ���
        Vector3 currentPos = transform.position;
        Vector3 midPoint = (currentPos + destination) / 2f;

        // NavMeshPath�� ����Ͽ� ���ο� ��� ���
        NavMeshPath newPath = new NavMeshPath();
        if (NavMesh.CalculatePath(currentPos, midPoint, NavMesh.AllAreas, newPath))
        {
            // ���ο� ��θ� ����
            nav.SetPath(newPath);
        }
    }

    private bool IsInMazeArea(Vector3 position)
    {
        // Ư�� �̷� ������ �����ϴ� ������ ������ ����
        // ���� ���, �̷� ���� ���� �ּ� �� �ִ� X �� Z ��ǥ ������ Ȯ��
        float minX = -5f;
        float maxX = 5f;
        float minZ = -5f;
        float maxZ = 0f;

        if (position.x >= minX && position.x <= maxX && position.z >= minZ && position.z <= maxZ)
        {
            return true;
        }

        return false;
    }

}
