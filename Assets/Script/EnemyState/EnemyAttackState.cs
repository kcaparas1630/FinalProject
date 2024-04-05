using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyAttackState : EnemyStateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        enemy.Agent.SetDestination(enemy.transform.position);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Get the direction to the player
        Vector3 directionToPlayer = enemy.Player.transform.position - enemy.transform.position;
        // Project the direction onto the X-Z plane
        directionToPlayer.y = 0f;
        // Rotate the enemy to face the player's position
        enemy.transform.rotation = Quaternion.LookRotation(directionToPlayer);
        if (enemy.GetDistanceFromPlayer() > enemy.AttackRangeStop && !enemy.hasCollidedWithFire)
        {
            animator.SetTrigger("Chase");
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
