using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonFlyChaseState : DragonStateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
      
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
     
        dragon.Agent.baseOffset = -2f;
        dragon.Agent.SetDestination(dragon.Player.transform.position);
        if (dragon.isDead)
        {
            animator.SetTrigger("Die");
        }
        else
        {
            if (dragon.isSwitch)
            {
                animator.SetTrigger("SwitchMode");
            }
            if (dragon.GetDistanceFromPlayer() < dragon.AttackRange)
            {
                animator.SetTrigger("FlyFlameAttack");
            }
        }
        
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        dragon.Agent.baseOffset = 0f;
    }
}
