using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyAttackCooldownState : EnemyStateMachineBehaviour
{
    private float timer;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        enemy.Agent.SetDestination(enemy.transform.position);
        timer = 0;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        // Get the direction to the player
        Vector3 directionToPlayer = enemy.Player.transform.position - enemy.transform.position;
        // Project the direction onto the X-Z plane
        directionToPlayer.y = 0f;
        // Rotate the enemy to face the player's position
        enemy.transform.rotation = Quaternion.LookRotation(directionToPlayer);
        if (timer > enemy.AttackCooldownTime)
        {
            animator.SetTrigger("Attack");
        }
    }
}
