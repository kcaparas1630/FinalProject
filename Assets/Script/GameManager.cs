using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private int totalTasks = 0;
    private int completedTasks = 0;
    [SerializeField] GameObject gateText;
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
        else
        {
            Debug.LogWarning("PassageBlock is not assigned in the GameManager!");
        }
    }
    private IEnumerator DisappearGateText()
    {
        // Wait for a certain duration (you can adjust the time)
        yield return new WaitForSeconds(5.0f); // Adjust the time as needed

        // Deactivate gateText
        gateText.SetActive(false);
    }
}
