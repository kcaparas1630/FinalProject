using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachineBehaviour : StateMachineBehaviour
{
    protected Enemy enemy;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.gameObject.GetComponent<Enemy>();
    }
}
