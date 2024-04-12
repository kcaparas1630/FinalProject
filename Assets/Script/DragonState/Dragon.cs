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
        Messenger.AddListener(GameEvent.BOSS_CUTSCENE_PLAYING, OnBossCutscenePlaying);
        Messenger.AddListener(GameEvent.BOSS_CUTSCENE_FINISHED, OnBossCutsceneFinished);
    }
    private void OnDestroy()
    {
        Messenger.AddListener(GameEvent.BOSS_DEATH, OnBossDeath);
        Messenger.RemoveListener(GameEvent.BOSS_CUTSCENE_PLAYING, OnBossCutscenePlaying);
        Messenger.AddListener(GameEvent.BOSS_CUTSCENE_FINISHED, OnBossCutsceneFinished);
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
        return Random.Range(0, 3);
    }
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

    IEnumerator SwitchMovementModeRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(15f); // Wait for 15 seconds
            isSwitch = true;
            yield return new WaitForSeconds(1f);
            isSwitch = false;
        }
    }
    private void OnBossCutscenePlaying()
    {
        Agent.enabled = false;
        Agent.isStopped = true;
        shouldFollowPlayer = false;
    }
    private void OnBossCutsceneFinished()
    {
        Agent.enabled = true;
        Agent.isStopped = false;
        shouldFollowPlayer = true;
    }
    //public bool IsPlayerWithinNavMeshBounds()
    //{
    //    NavMeshHit hit;
    //    Vector3 playerPosition = Player.transform.position;
    //    bool isWithinNavMesh = NavMesh.SamplePosition(playerPosition, out hit, 2.0f, NavMesh.AllAreas);

    //    if (isWithinNavMesh)
    //    {
    //        shouldFollowPlayer = true;
    //    }
    //    // Debug information
    //    if (!isWithinNavMesh)
    //    {
    //        Debug.LogWarning("Player position is not on the NavMesh.");
    //        Debug.Log("Player Position: " + playerPosition);
    //    }

    //    return isWithinNavMesh;
    //}


    //// Update is called once per frame
    //void Update()
    //{
    //    // Check if the player is within the NavMesh bounds
    //    if (IsPlayerWithinNavMeshBounds())
    //    {
    //        if (shouldFollowPlayer)
    //        {
    //            // Continue following the player if they are within bounds
    //            Agent.SetDestination(Player.transform.position);
    //        }
    //    }
    //    else
    //    {
    //        // Stop following the player if they are outside the NavMesh bounds
    //        shouldFollowPlayer = false;
    //        Agent.ResetPath(); // Clear the current path
    //    }
    //}

}
