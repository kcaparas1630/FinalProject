using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private int totalTasks = 0;
    private int completedTasks = 0;
    private int SkeysCollected = 0;
    private int GkeysCollected = 0;
    [SerializeField] GameObject gateText;
    [SerializeField] UpdateUIManager ui;
    public GameObject passageBlock;
 
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
    }
    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.KEY_PICKUP, CollectSKey);
        Messenger.RemoveListener(GameEvent.GOLDKEY_PICKUP, CollectGKey);
        Messenger.RemoveListener(GameEvent.OPEN_CHEST, UseSKey);
        Messenger.RemoveListener(GameEvent.OPEN_SCROLLCHEST, UseSKey);
        Messenger.RemoveListener(GameEvent.OPEN_MONSTERCHEST, UseSKey);
    }

    private void Start()
    {
        // Find all tasks
        GameObject[] tasks = GameObject.FindGameObjectsWithTag("Tasks");
        totalTasks = tasks.Length;
    }

    public void CompleteTask(GameObject exclamationMark)
    {
        if (exclamationMark != null)
        {
            
            // disable exclamationmark object
            exclamationMark.SetActive(false);

            // Increment completedTasks count
            completedTasks++;
            Debug.Log(completedTasks);

            // Check if all tasks are completed
            if (completedTasks >= totalTasks)
            {
                OpenGate();
                gateText.SetActive(true);
                StartCoroutine(DisappearGateText());
            }
        }
        else
        {
            Debug.LogWarning("ExclamationMark GameObject is null!");
        }

      
    }

    private void OpenGate()
    {
        // Check if the passageBlock is not null
        if (passageBlock != null)
        {
     
            Destroy(passageBlock);
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
    private IEnumerator DisappearGateText()
    {
        // Wait for 5 seconds
        yield return new WaitForSeconds(5.0f);

        // Deactivate gateText
        gateText.SetActive(false);
    }

}
