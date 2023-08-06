using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//çÏê¨ìríÜ
public class TuStateMachineBehaviour : StateMachineBehaviour
{
    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.IsName("UpMove"))
        {
            animator.ResetTrigger("Damage");
        }
    }

    // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
    //override public void OnStateUpdate(Animator playerAnimator, AnimatorStateInfo stateInfo, int layerIndex)
    //{

    //}

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.IsName("Waitstate"))
        {
            animator.ResetTrigger("WaitState");
        }
        if (stateInfo.IsName("UpMove"))
        {
            animator.ResetTrigger("Damage");
            animator.ResetTrigger("UpMove");
            animator.ResetTrigger("WaitState");
        }
    }

    // OnStateMove is called before OnStateMove is called on any state inside this state machine
    //override public void OnStateMove(Animator playerAnimator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateIK is called before OnStateIK is called on any state inside this state machine
    //override public void OnStateIK(Animator playerAnimator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMachineEnter is called when entering a state machine via its Entry Node
    //override public void OnStateMachineEnter(Animator playerAnimator, int stateMachinePathHash)
    //{
    //    
    //}

    // OnStateMachineExit is called when exiting a state machine via its Exit Node
    //override public void OnStateMachineExit(Animator playerAnimator, int stateMachinePathHash)
    //{
    //    
    //}
}
