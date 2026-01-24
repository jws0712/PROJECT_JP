using UnityEngine;

public class AttackReset : StateMachineBehaviour
{
    private Player player;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = animator.transform.root.GetComponent<Player>();
        player.EnableCombo();

        animator.ResetTrigger("Attack");
    }
}
