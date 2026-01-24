using UnityEngine;

public class PoliceNPC : NPCBase
{
    public Transform[] waypoints;
    private int currentWayPoint = 0;

    private void Update()
    {
        if (currentState == NPCState.Restrained || currentState == NPCState.Panicked) return;

        if (CanSeePlayer())
        {
            Attack();
        }
        else
        {
            FollowPatrolRoute();
        }
    }

    private void FollowPatrolRoute()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            currentWayPoint = (currentWayPoint + 1) % waypoints.Length;
            agent.SetDestination(waypoints[currentWayPoint].position);
        }
    }

    private void Attack()
    {
        currentState = NPCState.Combat;
        // 공격 로직 및 애니메이션
    }

    bool CanSeePlayer()
    {
        // 시야 체크 로직
        return false;
    }
}
