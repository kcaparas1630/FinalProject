using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundIdleState : DragonStateMachineBehaviour
{
    private float timer; //for tracking time passed in this state

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        timer = 0;
        dragon.Agent.SetDestination(dragon.transform.position);
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime; //increment time spent in idle
        if (timer > dragon.IdleTime)
        {
            animator.SetTrigger("Patrol");
        }
        if (timer > dragon.GroundTime)
        {
            animator.SetTrigger("Idle");
        }
        if(dragon.GetDistanceFromPlayer() > dragon.ChaseRange)
        {
            animator.SetTrigger("ChaseGround");
        }
    }
}
