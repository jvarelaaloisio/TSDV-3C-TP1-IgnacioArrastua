using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorBarrerRoll : StateMachineBehaviour
{
    private PlayerMovement playerMovement;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!playerMovement)
            playerMovement = animator.transform.parent.GetComponent<PlayerMovement>();

        playerMovement.RollMovement(true);


    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerMovement.RollMovement(false);
    }

}
