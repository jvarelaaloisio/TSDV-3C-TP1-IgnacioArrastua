
using UnityEngine;

public class AnimatorBarrerRoll : StateMachineBehaviour
{
    private PlayerMovement playerMovement;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!playerMovement)
            playerMovement = animator.transform.parent.GetComponent<PlayerMovement>();

        playerMovement.RollMovement(true);

    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerMovement.RollMovement(false);
    }

}
