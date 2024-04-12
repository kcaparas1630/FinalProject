using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private int totalTasks = 0;
    private int completedTasks = 0;
    private int SkeysCollected = 0;
    private int GkeysCollected = 0;
    private int crystalsDestroyed = 0;
    private int MAXCRYSTALS = 4;
    [SerializeField] GameObject gateText;
    [SerializeField] UpdateUIManager ui;
    [SerializeField] GameObject finalEventTrigger;
    [SerializeField] GameObject finalCrystal;
    [SerializeField] AudioSource dragonHurt;
    [SerializeField] PlayableDirector finalCutscene;
    private void Awake()
    {
        // Ensure there's only one instance of the GameManager
        //if (Instance == null)
        //{
        //    Instance = this;
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}
        Messenger.AddListener(GameEvent.KEY_PICKUP, CollectSKey);
        Messenger.AddListener(GameEvent.GOLDKEY_PICKUP, CollectGKey);
        Messenger.AddListener(GameEvent.OPEN_CHEST, UseSKey);
        Messenger.AddListener(GameEvent.OPEN_SCROLLCHEST, UseSKey);
        Messenger.AddListener(GameEvent.OPEN_MONSTERCHEST, UseSKey);
        Messenger.AddListener(GameEvent.TORCH_GRAB, CompleteTask);
        Messenger.AddListener(GameEvent.CLOSE_CANVAS, CompleteTask);
        Messenger.AddListener(GameEvent.TORCH_WAVE, OnTorchWave);
        Messenger.AddListener(GameEvent.FINAL_EVENT, OnFinalEvent);
        Messenger.AddListener(GameEvent.BOSSHEALTH_REDUCE, OnBossHealthReduce);
        
    }
    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.KEY_PICKUP, CollectSKey);
        Messenger.RemoveListener(GameEvent.GOLDKEY_PICKUP, CollectGKey);
        Messenger.RemoveListener(GameEvent.OPEN_CHEST, UseSKey);
        Messenger.RemoveListener(GameEvent.OPEN_SCROLLCHEST, UseSKey);
        Messenger.RemoveListener(GameEvent.OPEN_MONSTERCHEST, UseSKey);
        Messenger.RemoveListener(GameEvent.TORCH_GRAB, CompleteTask);
        Messenger.RemoveListener(GameEvent.CLOSE_CANVAS, CompleteTask);
        Messenger.RemoveListener(GameEvent.TORCH_WAVE, OnTorchWave);
        Messenger.RemoveListener(GameEvent.FINAL_EVENT, OnFinalEvent);
        Messenger.RemoveListener(GameEvent.BOSSHEALTH_REDUCE, OnBossHealthReduce);

    }
    private void OnBossHealthReduce()
    {
        crystalsDestroyed++;
        dragonHurt.Play();
        Debug.Log(crystalsDestroyed);
        if (crystalsDestroyed == 3)
        {
            finalCrystal.SetActive(true);
        }
        if (crystalsDestroyed == MAXCRYSTALS) {
            Messenger.Broadcast(GameEvent.BOSS_DEATH);
            finalCutscene.Play();
        }
    }
    private void OnFinalEvent()
    {
        finalEventTrigger.SetActive(true);
    }
    private void OnTorchWave()
    {
        Debug.Log("Do nothing");
    }
    private void Start()
    {
        // Find all tasks
        GameObject[] tasks = GameObject.FindGameObjectsWithTag("Tasks");
        totalTasks = tasks.Length;
    }

    public void CompleteTask()
    {
        // Increment completedTasks count
        completedTasks++;
        Debug.Log(completedTasks);
        // Check if all tasks are completed
        if (completedTasks >= totalTasks)
        {
            Messenger.Broadcast(GameEvent.BASEMENTDOOR_OPEN);
        }
 
    }

  

    public void CollectSKey()
    {
        SkeysCollected++;
        ui.UpdateSKeyCount(SkeysCollected);
    }
    public bool SKeyValidation()
    {
        return SkeysCollected > 0;
    }
    public void UseSKey()
    {
        if (SKeyValidation())
        {
            SkeysCollected--;
            ui.UpdateSKeyCount(SkeysCollected);
        }

    }
    public void CollectGKey()
    {
        GkeysCollected++;
        ui.UpdateGKeyCount(GkeysCollected);
    }
    public void UseGKey()
    {
        if (GkeysCollected > 0)
        {
            GkeysCollected--;
            ui.UpdateGKeyCount(GkeysCollected);
        }
        else
        {
            Debug.Log("No Golden Keys available");
        }
    }

}
