using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public enum NPCState
{
    Idle,       // 서 있음
    Patrol,     // 산책
    Panicked,   // 공포
    Fleeing,    // 도망
    Restrained  // 결박됨 (움직임 불가)
}

public class NPC : MonoBehaviour
{
    public NPCState currentState = NPCState.Idle;
    private NavMeshAgent agent;
    private Animator animator;

    public Transform[] waypoints;
    private int currentWayPoint = 0;

    public float waitTime = 2f;
    private float waitTimer = 0f;
    private bool isMovingForward = true;


    // Test
    public Transform player;
    
    
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        switch (currentState)
        {
            case NPCState.Idle:
                break;
            case NPCState.Patrol:
                UpdatePatrol();
                break;
            case NPCState.Panicked:
                // 제자리에 멈춰서 공포를 떪
                break;
            case NPCState.Fleeing:
                // 플레이어 반대 방향으로 도망
                break;
            case NPCState.Restrained:
                // 움직임 완전히 봉쇄
                break;
        }

        animator.SetFloat("Speed", agent.velocity.magnitude);

        if (Keyboard.current.qKey.isPressed)
        {
            SetState(NPCState.Patrol);
            Debug.Log("산책");
        }
        else if (Keyboard.current.wKey.isPressed)
        {
            FleeFrom(player.position);
            Debug.Log("도망");
        }
        else if (Keyboard.current.eKey.isPressed)
        {
            GetRestrained();
            Debug.Log("묶임");
        }
    }

    public void SetState(NPCState newState)
    {
        currentState = newState;

        if(newState == NPCState.Restrained || newState == NPCState.Panicked)
        {
            agent.isStopped = true;
        }
        else
        {
            agent.isStopped = false;
        }

        // 애니메이터 파라미터 업데이트
    }

    // 산책
    private void UpdatePatrol()
    {
        if(!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            currentWayPoint = (currentWayPoint + 1) % waypoints.Length;
            agent.SetDestination(waypoints[currentWayPoint].position);
        }
    }

    // 도망
    public void FleeFrom(Vector3 dangerSource)
    {
        SetState(NPCState.Fleeing);
        Vector3 runDirection = transform.position - dangerSource;
        Vector3 runTo = transform.position + runDirection.normalized * 10f;
        agent.SetDestination(runTo);
    }

    public void GetRestrained()
    {
        SetState(NPCState.Restrained);
        //agent.enabled = false;
        // 손이 묶이 모델 활성화
    }

    private void GoToNextWaypoint()
    {
        if (isMovingForward)
        {
            currentWayPoint++;
            if(currentWayPoint >= waypoints.Length)
            {
                currentWayPoint = waypoints.Length - 2;
                isMovingForward = false;
            }
        }
        else
        {
            currentWayPoint--;
            if(currentWayPoint < 0)
            {
                currentWayPoint = 1;
                isMovingForward = true;
            }
        }
        
        agent.SetDestination(waypoints[currentWayPoint].position);
    }
}
