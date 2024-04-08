using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScaredState : EnemyStateMachineBehaviour
{
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        enemy.Agent.SetDestination(enemy.transform.position);
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy.StartCooldownCoroutine();
        if(enemy.GetDistanceFromPlayer() < enemy.AttackRange && !enemy.hasCollidedWithFire)
        {
            Debug.Log("Attack");
            animator.SetTrigger("Attack");
        }
        if(enemy.GetDistanceFromPlayer() > enemy.AttackRangeStop && !enemy.hasCollidedWithFire && !enemy.playerUnderBed)
        {
            Debug.Log("Chase");
            animator.SetTrigger("Chase");
        }
        if(enemy.playerUnderBed)
        {
            animator.SetTrigger("Patrol");
        }
    }
    
}
