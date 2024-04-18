using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DragonGroundChaseState : DragonStateMachineBehaviour
{

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        //chooses a random attack and assign it into variable
        int randomAttackMode = dragon.ChooseRandomAttack();
        //pass in randomAttackMode as an argument to be passed in to ExecuteAttack method
        dragon.ExecuteAttack(randomAttackMode);
        dragon.Agent.SetDestination(dragon.Player.transform.position);
        if(dragon.isDead)
        {
            animator.SetTrigger("Die");
        }
        else
        {
            if (dragon.isSwitch)
            {
                animator.SetTrigger("SwitchMode");
            }
            if (dragon.GetDistanceFromPlayer() < dragon.AttackRange && dragon.attackMode == Dragon.AttackMode.MouthAttack)
            {
                animator.SetTrigger("GroundBasicAttack");
            }
            else if (dragon.GetDistanceFromPlayer() < dragon.AttackRange && dragon.attackMode == Dragon.AttackMode.ClawAttack)
            {
                animator.SetTrigger("GroundClawAttack");
            }
            else if (dragon.GetDistanceFromPlayer() < dragon.AttackRange && dragon.attackMode == Dragon.AttackMode.FlameAttack)
            {
                animator.SetTrigger("GroundFlameAttack");
            }
        }
        
    }
}
