using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpookyActivate : StateMachineBehaviour
{
    [SerializeField]
    private float _ticks = 0;

    [SerializeField]
    public float MinActivateTime = 5.0f;
    [SerializeField]
    public float MaxActivateTime = 8.0f;

    private float _activateTime = 5.0f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        this._ticks += Time.deltaTime;
        if (this._ticks > _activateTime)
        {
            animator.SetBool("isActive", true);
            this._ticks = 0;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        this._activateTime = Random.Range(MinActivateTime, MaxActivateTime);
        animator.SetBool("isActive", false);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
