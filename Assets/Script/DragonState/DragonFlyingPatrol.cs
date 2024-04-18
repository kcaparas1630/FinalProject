using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonFlyingPatrol : DragonStateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        dragon.DetermineNextWaypoint();
        dragon.Agent.SetDestination(dragon.GetCurrentWaypoint());
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        dragon.Agent.baseOffset = -2f;//makes it fly lower
        if (dragon.Agent.remainingDistance <= dragon.Agent.stoppingDistance)
        {
            animator.SetTrigger("FlyIdle");
        }
        if (dragon.GetDistanceFromPlayer() < dragon.ChaseRange)
        {
            animator.SetTrigger("ChaseFly");
        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        dragon.Agent.baseOffset = 0f; // set back to 0 to prevent unecessary behaviour
    }
}
