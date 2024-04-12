using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonStateMachineBehaviour : StateMachineBehaviour
{
    protected Dragon dragon;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        dragon = animator.gameObject.GetComponent<Dragon>();
    }
}
