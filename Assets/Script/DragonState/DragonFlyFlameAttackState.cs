using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonFlyFlameAttackState : DragonStateMachineBehaviour
{
    private float timer;
    private float attackAngle = 50f;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        dragon.Agent.SetDestination(dragon.transform.position);
        timer = 0;
        dragon.startFlameThrower();
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
            if (timer > dragon.AttackCooldownTime && dragon.GetDistanceFromPlayer() < dragon.AttackRange)
            {
                animator.SetTrigger("AttackCooldown");
            }
            if (dragon.GetDistanceFromPlayer() > dragon.AttackRangeStop || dragon.isSwitch)
            {
                animator.SetTrigger("ChaseFly");
            }
        }
        
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        dragon.endFlameThrower();
    }
}
