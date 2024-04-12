using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAttackCooldownGroundState : DragonStateMachineBehaviour
{
    private float timer;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        dragon.Agent.SetDestination(dragon.transform.position);
        timer = 0;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        //// Get the direction to the player
        //Vector3 directionToPlayer = dragon.Player.transform.position - dragon.transform.position;
        //// Project the direction onto the X-Z plane
        //directionToPlayer.y = 0f;
        //// Rotate the enemy to face the player's position
        //dragon.transform.rotation = Quaternion.LookRotation(directionToPlayer);
        int randomAttackMode = dragon.ChooseRandomAttack();
        dragon.ExecuteAttack(randomAttackMode);
        if (dragon.isDead)
        {
            animator.SetTrigger("Die");
        }
        else
        {
            if (timer > dragon.AttackCooldownTime && dragon.attackMode == Dragon.AttackMode.MouthAttack)
            {
                animator.SetTrigger("GroundBasicAttack");
            }
            else if (timer > dragon.AttackCooldownTime && dragon.attackMode == Dragon.AttackMode.ClawAttack)
            {
                animator.SetTrigger("GroundClawAttack");
            }
            else if (timer > dragon.AttackCooldownTime && dragon.attackMode == Dragon.AttackMode.FlameAttack)
            {
                animator.SetTrigger("GroundFlameAttack");
            }
        }
        
    }
}