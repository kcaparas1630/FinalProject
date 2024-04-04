using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Monster : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;    // for moving on the NavMesh
    [SerializeField] private Animator anim;
    [SerializeField] private Transform target;      // the target to follow
    [SerializeField] private AudioSource aliveScream;
    [SerializeField] private AudioSource attackScream;
    private float distanceToTarget = float.MaxValue;
    private float chaseRange = 10f;
    public float attackDistance = 2f;
    private float rotationSpeed = 5f;
    private bool hasAttacked = false;
    private enum EnemyState { Idle, Chase, Attack, Dead };
    private EnemyState state;

    private void SetState(EnemyState newState)
    {
        state = newState;
    }

    void Start()
    {
        SetState(EnemyState.Idle);      // start off in the Alive state
        StartCoroutine(EnemyDead());
    }

    void Update()
    {
        distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        // what happens here depends on the state we're currently in!
        switch (state)
        {
            case EnemyState.Idle: Update_Idle(); break;
            case EnemyState.Chase: Update_Chase(); break;
            case EnemyState.Attack: Update_Attack(); break;
            case EnemyState.Dead: Update_Dead(); break;
            default: Debug.Log("Invalid state!"); break;
        }
    }

    void Update_Idle()
    {
        agent.isStopped = true;                             // stop the agent (following)
        if (distanceToTarget <= chaseRange)
        {
            SetState(EnemyState.Chase);
        }
    }
    void Update_Chase()
    {
        agent.isStopped = false;                            // start the agent (following)
        agent.SetDestination(target.transform.position);    // follow the target
        float velocityMagnitude = agent.velocity.magnitude;
        anim.SetFloat("Velocity", velocityMagnitude);
        if (distanceToTarget <= attackDistance)
        {
            SetState(EnemyState.Attack);
        }
        else if (distanceToTarget > chaseRange)
        {
            SetState(EnemyState.Idle);
        }
    }
    void Update_Attack()
    {

        if(!hasAttacked)
        {
            StartCoroutine(AttackCooldown());

        }
        if (distanceToTarget > attackDistance)
        {
            SetState(EnemyState.Chase);
        }
    }
    void Update_Dead()
    {
        Debug.Log("Enemy Dead");
        anim.SetTrigger("Die");
    }
    IEnumerator AttackCooldown()
    {
        hasAttacked = true;
        Vector3 directionToTarget = target.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        anim.SetTrigger("Attack");
        if (!attackScream.isPlaying)
        {
            attackScream.Play();
        }
        yield return new WaitForSeconds(5f);
        hasAttacked = false;
        Debug.Log("attacked");
        Messenger.Broadcast(GameEvent.PLAYER_HIT);
    }
    IEnumerator EnemyDead()
    {
        yield return new WaitForSeconds(20f);
        SetState(EnemyState.Dead);
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }

}
