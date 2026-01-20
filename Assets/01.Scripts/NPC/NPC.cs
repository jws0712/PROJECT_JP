using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    public Transform[] waypoints;
    public float waitTime = 2f;

    private NavMeshAgent agent;
    private int currentWayPoint = 0;
    private float waitTimer = 0f;
    private bool isMovingForward = true;
    private Animator animator;
    
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if(waypoints.Length > 0)
        {
            agent.SetDestination(waypoints[currentWayPoint].position);
        }
    }

    private void Update()
    {
        if(!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            waitTimer += Time.deltaTime;
            if(waitTimer > waitTime)
            {
                waitTimer = 0;
                GoToNextWaypoint();
            }
        }

        animator.SetFloat("Speed", agent.velocity.magnitude);
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
