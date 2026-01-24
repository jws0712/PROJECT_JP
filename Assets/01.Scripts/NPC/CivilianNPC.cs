using UnityEngine;
using UnityEngine.InputSystem;

public class CivilianNPC : NPCBase
{
    public Transform[] waypoints;
    private int currentWayPoint = 0;

    private void Update()
    {
        switch (currentState)
        {
            case NPCState.Idle:
                PerformDailyRoutine();
                break;
            case NPCState.Patrol:
                PerformDailyRoutine();
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
        else if (Keyboard.current.rKey.isPressed)
        {
            GetPanicked();
            Debug.Log("공포");
        }
    }

    private void PerformDailyRoutine()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            currentWayPoint = (currentWayPoint + 1) % waypoints.Length;
            agent.SetDestination(waypoints[currentWayPoint].position);
        }
    }

    private void FleeFrom(Vector3 dangerSource)
    {
        SetState(NPCState.Fleeing);
        Vector3 runDirection = transform.position - dangerSource;
        Vector3 runTo = transform.position + runDirection.normalized * 10f;
        agent.SetDestination(runTo);
    }
}
