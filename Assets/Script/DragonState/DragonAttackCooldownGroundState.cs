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
        //chooses a random attack and assign it into variable
        int randomAttackMode = dragon.ChooseRandomAttack();
        //pass in randomAttackMode as an argument to be passed in to ExecuteAttack method
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
