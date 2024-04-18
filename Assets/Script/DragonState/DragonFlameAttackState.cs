using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonFlameAttackState : DragonStateMachineBehaviour
{
    private float timer;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        dragon.Agent.SetDestination(dragon.Player.transform.position);
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
                animator.SetTrigger("ChaseGround");
            }
        }
        
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        dragon.endFlameThrower();
    }
}
