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
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }


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
        if (SkeysCollected >= 1)
        {
            return true;
        }
        return false;
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
