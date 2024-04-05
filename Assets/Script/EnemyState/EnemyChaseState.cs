using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyStateMachineBehaviour
{
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy.Agent.SetDestination(enemy.Player.transform.position);
        if(enemy.GetDistanceFromPlayer() < enemy.AttackRange && !enemy.hasCollidedWithFire)
        {
            animator.SetTrigger("Attack");
        }
        if (enemy.hasCollidedWithFire)
        {
            animator.SetTrigger("Duck");
            enemy.StartCooldownCoroutine();
        }
        if (enemy.playerUnderBed)
        {
            animator.SetTrigger("Patrol");
        }
    }
}
