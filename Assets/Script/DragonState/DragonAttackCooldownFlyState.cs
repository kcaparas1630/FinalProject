using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAttackCooldownFlyState : DragonStateMachineBehaviour
{
    private float timer;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        //flag trigger to prevent dragon from following player when on cutscene.
        if (dragon.shouldFollowPlayer)
        {
            dragon.Agent.SetDestination(dragon.transform.position);
        }
       
        timer = 0;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        if (dragon.isDead)
        {
            animator.SetTrigger("Die");
        }
        else
        {
            if (timer > dragon.AttackCooldownTime)
            {
                animator.SetTrigger("FlyFlameAttack");
            }
        }
        
    }
}
