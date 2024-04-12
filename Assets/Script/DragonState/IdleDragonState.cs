using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleDragonState : DragonStateMachineBehaviour
{
    private float timer; //for tracking time passed in this state

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        timer = 0;
        dragon.Agent.SetDestination(dragon.transform.position);
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;// update time passed
        if(timer > dragon.IdleTime)
        {

            int randomMovementMode = dragon.ChooseMovementMode();
            dragon.ExecuteMovement(randomMovementMode);
            if (dragon.movementMode == Dragon.MovementMode.OnFoot)
            {
                animator.SetTrigger("GroundIdle");
            }
            else
            {
                animator.SetTrigger("FlyIdle");
            }
        }
    }

}
