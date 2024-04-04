using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : EnemyStateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        enemy.DetermineNextWaypoint();
        enemy.Agent.SetDestination(enemy.GetCurrentWaypoint());
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(enemy.Agent.remainingDistance <= enemy.Agent.stoppingDistance)
        {
            animator.SetTrigger("Idle");
        }
    }
}
