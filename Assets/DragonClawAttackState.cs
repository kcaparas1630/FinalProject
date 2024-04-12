using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonClawAttackState : DragonStateMachineBehaviour
{
    private float timer;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        // Only set the destination if shouldFollowPlayer is true
        if (dragon.shouldFollowPlayer)
        {
            dragon.Agent.SetDestination(dragon.Player.transform.position);
        }
        timer = 0;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //// Get the direction to the player
        //Vector3 directionToPlayer = dragon.Player.transform.position - dragon.transform.position;
        //// Project the direction onto the X-Z plane
        //directionToPlayer.y = 0f;
        //// Rotate the enemy to face the player's position
        //dragon.transform.rotation = Quaternion.LookRotation(directionToPlayer);
        timer += Time.deltaTime;
        if (dragon.isDead)
        {
            animator.SetTrigger("Die");
        }
        else
        {
            if (timer > dragon.AttackCooldownTime && dragon.GetDistanceFromPlayer() < dragon.AttackRange)
            {
                animator.SetTrigger("AttackCooldown");
            }
            if (dragon.GetDistanceFromPlayer() > dragon.AttackRangeStop || dragon.isSwitch)
            {
                animator.SetTrigger("ChaseGround");
            }
        }
        
    }
}
