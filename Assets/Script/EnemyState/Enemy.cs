using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    public float IdleTime { get; private set; } = 3.0f;         // time to spend in idle state
    public float AttackCooldownTime { get; private set; } = 2.5f; // time to spend in cooldown
    public float ChaseRange { get; private set; } = 7.0f;
    public float AttackRange { get; private set; } = 3.0f;      // when player is closer than this, attack
    public float AttackRangeStop { get; private set; } = 7.0f; // when player is farther than this, chase
    public List<Transform> Waypoints { get; private set; }      // waypoints for patrol state
    private int waypointIndex = 0;                              // current waypoint index
    public float rotationSpeed { get; private set; } = 5.0f;
    public bool hasCollidedWithFire { get; set; } = false;
    public bool playerUnderBed { get; set; } = false;
    public bool hasAttacked { get; set; } = true;
    public GameObject Player { get; private set; }
    public NavMeshAgent Agent { get; private set; }

    [SerializeField] private AudioSource attackSound;
    // Start is called before the first frame update
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();                   // get a reference to the NavMeshAgent
        Agent.updateUpAxis = false;
        Player = GameObject.FindGameObjectWithTag("Player");    // get reference to Player
        // Create and populate a list of waypoints
        Waypoints = new List<Transform>();
        GameObject waypointsParent = GameObject.FindGameObjectWithTag("Waypoints");
        foreach (Transform t in waypointsParent.transform)
        {
            Waypoints.Add(t);
        }
    }

    private void Awake()
    {
        Messenger.AddListener(GameEvent.TORCH_WAVE, OnTorchWave);
        Messenger.AddListener(GameEvent.QUARTERS_CUTSCENE_PLAYING, OnQuartersCutscenePlaying);
        Messenger.AddListener(GameEvent.QUARTERS_CUTSCENE_FINISHED, OnQuartersCutsceneFinished);
        Messenger.AddListener(GameEvent.UNDER_BED, OnUnderBed);
        Messenger.AddListener(GameEvent.EXIT_BED, OnExitBed);
    }
    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.TORCH_WAVE, OnTorchWave);
        Messenger.RemoveListener(GameEvent.QUARTERS_CUTSCENE_PLAYING, OnQuartersCutscenePlaying);
        Messenger.RemoveListener(GameEvent.QUARTERS_CUTSCENE_FINISHED, OnQuartersCutsceneFinished);
        Messenger.RemoveListener(GameEvent.UNDER_BED, OnUnderBed);
        Messenger.AddListener(GameEvent.EXIT_BED, OnExitBed);
    }

    //public void isCooldownFinish()
    //{
    //    StartCoroutine(AttackCountdown());
       
    //}
    //IEnumerator AttackCountdown()
    //{
    //    yield return new WaitForSeconds(2.5f);
    //    hasAttacked = false;
     
    //}
    public void playAttackSound()
    {
        if(!attackSound.isPlaying)
        {
            attackSound.Play();
        }
    }
    private void OnUnderBed()
    {
        playerUnderBed = true;
    }
    private void OnExitBed()
    {
        playerUnderBed = false;
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

    private void OnTorchWave()
    {
        if(GetDistanceFromPlayer() < AttackRange)
        {
            Debug.Log("Scare initiate");
            hasCollidedWithFire = true;
        }
        
    }
    IEnumerator ChangeStateCooldown()
    {
        yield return new WaitForSeconds(5f);
        hasCollidedWithFire = false;
    }

    // Method to invoke the coroutine from outside
    public void StartCooldownCoroutine()
    {
        StartCoroutine(ChangeStateCooldown());
    }
    
    private void OnQuartersCutscenePlaying()
    {
        Agent.enabled = false;
    }
    private void OnQuartersCutsceneFinished()
    {
        Agent.enabled = true;
    }
}
