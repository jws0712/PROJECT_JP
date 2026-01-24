using UnityEngine;
using UnityEngine.AI;

public enum NPCState
{
    Idle,       // 서 있음
    Patrol,     // 산책
    Panicked,   // 공포
    Fleeing,    // 도망
    Restrained, // 결박됨 (움직임 불가)
    Combat      // 공격
}

public class NPCBase : MonoBehaviour
{
    public NPCState currentState = NPCState.Idle;
    protected NavMeshAgent agent;
    protected Animator animator;

    // Test
    public Transform player;

    public virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // 공통: 상태 변경
    public void SetState(NPCState newState)
    {
        currentState = newState;
        animator.SetBool("isPanicked", false);
        animator.SetBool("isRestrained", false);

        if (newState == NPCState.Restrained || newState == NPCState.Panicked)
        {
            agent.isStopped = true;
        }
        else
        {
            agent.isStopped = false;
        }

        // 애니메이터 파라미터 업데이트
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    // 공통: 공포
    public virtual void GetPanicked()
    {
        if (currentState == NPCState.Restrained) return;
        SetState(NPCState.Panicked);
        agent.isStopped = true;
        animator.SetBool("isPanicked", true);
    }

    // 공통: 결박
    public virtual void GetRestrained()
    {
        SetState(NPCState.Restrained);
        agent.isStopped = true;
        animator.SetBool("isRestrained", true);
    }
}
