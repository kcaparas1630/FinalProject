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
        Messenger.AddListener(GameEvent.KEY_PICKUP, CollectSKey);
        Messenger.AddListener(GameEvent.GOLDKEY_PICKUP, CollectGKey);
        Messenger.AddListener(GameEvent.OPEN_CHEST, UseSKey);
        Messenger.AddListener(GameEvent.OPEN_SCROLLCHEST, UseSKey);
        Messenger.AddListener(GameEvent.OPEN_MONSTERCHEST, UseSKey);
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
        Messenger.RemoveListener(GameEvent.TORCH_WAVE, OnTorchWave);
        Messenger.RemoveListener(GameEvent.FINAL_EVENT, OnFinalEvent);
        Messenger.RemoveListener(GameEvent.BOSSHEALTH_REDUCE, OnBossHealthReduce);

    }
    private void OnBossHealthReduce()
    {
        crystalsDestroyed++;//increment by 1 per crystal destroy
        dragonHurt.Play();//sound trigger when a crystal destroyed
        if (crystalsDestroyed == 3)
        {
            finalCrystal.SetActive(true);
        }
        if (crystalsDestroyed == MAXCRYSTALS) {
            //Trigger death for boss, play animation of death.
            Messenger.Broadcast(GameEvent.BOSS_DEATH);
            StartCoroutine(playFinalCutscene());
        }
    }
    IEnumerator playFinalCutscene()
    {
        yield return new WaitForSeconds(2f);
        finalCutscene.Play();
        yield return new WaitForSeconds(13f);
        Messenger.Broadcast(GameEvent.GAME_FINISHED);
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

  

    public void CollectSKey()
    {
        SkeysCollected++;
        ui.UpdateSKeyCount(SkeysCollected);
    }
    // checks if num of skey is more than 0
    public bool SKeyValidation()
    {
        return SkeysCollected > 0;
    }
    //For any golden key or special chests, checks for validation using SKeyValidation method
    public void UseSKey()
    {
        if (SKeyValidation())
        {
            SkeysCollected--;
            ui.UpdateSKeyCount(SkeysCollected);
        }

    }
    //increments gold keys count
    public void CollectGKey()
    {
        GkeysCollected++;
        ui.UpdateGKeyCount(GkeysCollected);
    }
    //use gkey if more than 0
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
