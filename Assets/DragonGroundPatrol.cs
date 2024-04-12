using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonGroundPatrol : DragonStateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        dragon.DetermineNextWaypoint();
        dragon.Agent.SetDestination(dragon.GetCurrentWaypoint());
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (dragon.Agent.remainingDistance <= dragon.Agent.stoppingDistance)
        {
            animator.SetTrigger("GroundIdle");
        }
        if (dragon.GetDistanceFromPlayer() < dragon.ChaseRange)
        {
            animator.SetTrigger("ChaseGround");
        }
    }
}
