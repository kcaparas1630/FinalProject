using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyStateMachineBehaviour
{
    private float timer; //for tracking time passed in this state

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        timer = 0;
        enemy.Agent.SetDestination(enemy.transform.position);
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime; // update time passed
        if(timer > enemy.IdleTime)
        {
            animator.SetTrigger("Patrol");
        }
        if(enemy.GetDistanceFromPlayer() < enemy.ChaseRange && !enemy.playerUnderBed)
        {
            animator.SetTrigger("Chase");
        }
    }
}
