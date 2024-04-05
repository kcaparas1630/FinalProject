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
        if(enemy.GetDistanceFromPlayer() < enemy.AttackRange && !enemy.hasCollidedWithFire)
        {
            Debug.Log("Attack");
            animator.SetTrigger("Attack");
        }
        else if(enemy.GetDistanceFromPlayer() > enemy.AttackRangeStop && !enemy.hasCollidedWithFire)
        {
            Debug.Log("Chase");
            animator.SetTrigger("Chase");
        }
    }
    
}
