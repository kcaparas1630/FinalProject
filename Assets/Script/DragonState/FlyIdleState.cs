using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyIdleState : DragonStateMachineBehaviour
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
        dragon.Agent.baseOffset = -2f;
        if (timer > dragon.IdleTime)
        {
            animator.SetTrigger("Patrol");
        }
        if (timer > dragon.FlyingTime)
        {
            animator.SetTrigger("Land");
        }
        if (dragon.GetDistanceFromPlayer() > dragon.ChaseRange)
        {
            animator.SetTrigger("ChaseFly");
        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        dragon.Agent.baseOffset = 0f;
    }
}
