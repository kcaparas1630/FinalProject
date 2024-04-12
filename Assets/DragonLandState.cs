using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonLandState : DragonStateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        dragon.Agent.SetDestination(dragon.transform.position);
        animator.SetTrigger("Idle");
    }
}
