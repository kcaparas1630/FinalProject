using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dragon : MonoBehaviour
{
    public float IdleTime { get; private set; } = 1.0f;         // time to spend in idle state
    public float GroundTime { get; private set; } = 8.0f; // time spent in ground state
    public float FlyingTime { get; private set; } = 8.0f; // time spend in flying state
    public float AttackCooldownTime { get; private set; } = 2.5f; // time to spend in cooldown
    public float ChaseRange { get; private set; } = 7.0f;
    public float AttackRange { get; private set; } = 5.0f;      // when player is closer than this, attack
    public float AttackRangeStop { get; private set; } = 7.0f; // when player is farther than this, chase
    public List<Transform> Waypoints { get; private set; }      // waypoints for patrol state
    private int waypointIndex = 0;
    public float rotationSpeed { get; private set; } = 5.0f;
    public GameObject Player { get; private set; }
    public NavMeshAgent Agent { get; private set; }
    [SerializeField] ParticleSystem flameThrower;
    [SerializeField] GameObject flameParticleSystem;
    public enum MovementMode { OnFoot, Flying }
    public MovementMode movementMode = MovementMode.OnFoot;
    public enum AttackMode { MouthAttack,ClawAttack,FlameAttack};
    public AttackMode attackMode = AttackMode.MouthAttack;
    public bool shouldFollowPlayer = false;
    public bool isSwitch = false;
    public bool isDead = false;
    private void Start()
    {

        Agent = GetComponent<NavMeshAgent>();                   // get a reference to the NavMeshAgent
        Agent.updateUpAxis = false;
        Player = GameObject.FindGameObjectWithTag("Player");    // get reference to Player
        StartCoroutine(SwitchMovementModeRoutine());
        // Create and populate a list of waypoints
        Waypoints = new List<Transform>();
        GameObject waypointsParent = GameObject.FindGameObjectWithTag("Waypoints2");
        foreach (Transform t in waypointsParent.transform)
        {
            Waypoints.Add(t);
        }
    }
    private void Awake()
    {
        Messenger.AddListener(GameEvent.BOSS_DEATH, OnBossDeath);
    }
    private void OnDestroy()
    {
        Messenger.AddListener(GameEvent.BOSS_DEATH, OnBossDeath);
      
    }
    private void OnBossDeath()
    {
        isDead = true;
    }
    public void DetermineNextWaypoint()
    {
        //pick a random waypoint
        waypointIndex = Random.Range(0, Waypoints.Count);
    }

    public Vector3 GetCurrentWaypoint()
    {
        //return the current waypoint
        return Waypoints[waypointIndex].position;
    }
    public float GetDistanceFromPlayer()
    {
        //Get the distance(in units) from the enemy to the player
        return Vector3.Distance(transform.position, Player.transform.position);
    }
   
    public void startFlameThrower()
    {
        flameParticleSystem.SetActive(true);
        flameThrower.Play();
    }
    public void endFlameThrower()
    {
        flameThrower.Stop();
        flameParticleSystem.SetActive(false);
    }
    public int ChooseMovementMode()
    {
        return Random.Range(0,2); // return 0 and 1 exclusively
    }
    //takes parameter value from method ChooseMovementMode
    public void ExecuteMovement(int movementIndex)
    {
        switch(movementIndex)
        {
            case 0:
                movementMode = MovementMode.OnFoot;
                break;
            case 1:
                movementMode = MovementMode.Flying;
                break;
            default:
                Debug.Log("Invalid MovementIndex");
                break;
        }
    }
   
    public int ChooseRandomAttack()
    {
        return Random.Range(0, 3);// return 0 to 2 exclusively
    }
    //takes parameter value from ChooseRandomAttack method
    public void ExecuteAttack(int attackIndex)
    {
        switch(attackIndex)
        {
            case 0:
                attackMode = AttackMode.MouthAttack;
                break;
            case 1:
                attackMode = AttackMode.ClawAttack;
                break;
            case 2:
                attackMode= AttackMode.FlameAttack;
                break;
            default:
                Debug.Log("Invalid AttackIndex");
                break;

        }
    }
    // changes movementMode from any state after 15 seconds.
    IEnumerator SwitchMovementModeRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(15f); // Wait for 15 seconds
            isSwitch = true;
            yield return new WaitForSeconds(1f);
            //returns to false to cycle back
            isSwitch = false;
        }
    }

}
