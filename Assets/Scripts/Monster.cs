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
    private Animator Animator;

    private bool isAction;  // 행동 중인지 아닌지 판별
    private bool isWalking; // 걷는지, 안 걷는지 판별
    private bool isChasing;
    private bool isWaiting = true;

    [SerializeField] private float walkTime;  // 걷기 시간
    [SerializeField] private float walkSpeed;  // 걷기 속력
    [SerializeField] private float waitTime;  // 대기 시간
    [SerializeField] private float chaseSpeed;  // 추격 속력
    [SerializeField] private float chaseTime;  // 최대 추격 시간
    [SerializeField] protected float currentChaseTime;
    [SerializeField] protected float chaseDelayTime;

    public float raycastDistance = 5.0f;  // 레이의 길이
    public LayerMask wallMask;  // 벽 레이어 마스크

    private float currentTime;
    private static readonly int HashIswalking = Animator.StringToHash("IsWalking");
    private static readonly int HashIsChasing = Animator.StringToHash("IsChasing");

    private HashSet<Vector3> visitedLocations = new HashSet<Vector3>(); // 이미 방문한 위치 저장


    void Start()
    {
        currentTime = waitTime;
        isAction = true;   // 대기도 행동
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
                // 미로 구간 내에 있다면 다른 목표 지점을 설정
                SetNewRandomDestination();
            }
            else
            {
            // 벽을 감지하고 회피 경로 계산
                if (DetectWall())
                {
                    CalculateAvoidancePath();
                }
                else
                {
                    // NavMesh Agent를 사용하여 목표 지점으로 이동
                    nav.SetDestination(destination);

                    // 목적지가 유효한 경우 방문한 위치에 추가
                    visitedLocations.Add(destination);
                }

                // NavMesh Agent가 목표 지점에 도달했을 때 새로운 목표 지점 설정
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
            if (currentTime <= 0 && !isChasing)  // 랜덤하게 다음 행동을 개시
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
            // 새로운 랜덤 목표 지점 설정
            newDestination = new Vector3(Random.Range(-110f, 110f), 0f, Random.Range(-110f, 110f));
        } while (visitedLocations.Contains(newDestination)); // 이미 방문한 위치라면 다시 설정

        destination = newDestination;

        // 최단 경로 계산
        if (nav.CalculatePath(destination, new NavMeshPath()))
        {
            isWalking = true;
            Animator.SetBool(HashIswalking, isWalking);
        }
        else
        {
            // 목표 지점이 접근 불가능한 경우 다시 설정
            SetNewRandomDestination();
        }
    }

    protected void TryWalk()
    {
        currentTime = walkTime;
        isWalking = true;
        Animator.SetBool(HashIswalking, isWalking);
        nav.speed = walkSpeed;
        //Debug.Log("걷기");
    }

    private void Wait()
    {
        currentTime = waitTime;
        Animator.SetBool(HashIswalking, false);
        nav.ResetPath();
        //Debug.Log("대기");
    }

    public void Chase(Vector3 _targetPos)
    {
        isChasing = true;
        destination = _targetPos;
        nav.SetDestination(destination);
        Animator.SetBool(HashIsChasing, isChasing);
        nav.speed = chaseSpeed;
        //Debug.Log("추격중!");
    }
    private bool DetectWall()
    {
        // Raycast를 사용하여 벽을 감지
        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 wallDirection = Quaternion.Euler(0, 45, 0) * forward; // 45도 회전된 방향

        if (Physics.Raycast(transform.position, wallDirection, out hit, raycastDistance, wallMask))
        {
            // 벽을 감지한 경우
            Debug.DrawRay(transform.position, wallDirection * raycastDistance, Color.red);
            return true;
        }
        else
        {
            // 벽을 감지하지 않은 경우
            Debug.DrawRay(transform.position, wallDirection * raycastDistance, Color.green);
            return false;
        }
    }
    private void CalculateAvoidancePath()
    {
        // 현재 위치와 원래 목표 지점 간의 중간 지점을 계산
        Vector3 currentPos = transform.position;
        Vector3 midPoint = (currentPos + destination) / 2f;

        // NavMeshPath를 사용하여 새로운 경로 계산
        NavMeshPath newPath = new NavMeshPath();
        if (NavMesh.CalculatePath(currentPos, midPoint, NavMesh.AllAreas, newPath))
        {
            // 새로운 경로를 설정
            nav.SetPath(newPath);
        }
    }

    private bool IsInMazeArea(Vector3 position)
    {
        // 특정 미로 구간을 포함하는 영역의 조건을 설정
        // 예를 들어, 미로 구간 내의 최소 및 최대 X 및 Z 좌표 범위를 확인
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
