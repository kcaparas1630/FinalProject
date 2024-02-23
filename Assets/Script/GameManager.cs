using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int totalTasks = 0;
    private int completedTasks = 0;

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
        // Find all tasks in the scene and count them
        GameObject[] tasks = GameObject.FindGameObjectsWithTag("Tasks");
        totalTasks = tasks.Length;
    }

    public void CompleteTask(GameObject exclamationMark)
    {
        if (exclamationMark != null)
        {
            
            // Disable or destroy the Exclamation_Yellow GameObject
            exclamationMark.SetActive(false);

            // Increment completedTasks count
            completedTasks++;
            Debug.Log(completedTasks);

            // Check if all tasks are completed
            if (completedTasks >= totalTasks)
            {
                Debug.Log("Gate has opened");
                OpenGate();
            }
        }
        else
        {
            Debug.LogWarning("ExclamationMark GameObject is null!");
        }

        // You can add additional logic here for specific task completion actions.
    }

    private void OpenGate()
    {
        // Check if the passageBlock is not null
        if (passageBlock != null)
        {
            // Assuming PassageBlock is a GameObject, you can disable it or destroy it
            Destroy(passageBlock);
        }
        else
        {
            Debug.LogWarning("PassageBlock is not assigned in the GameManager!");
        }
    }
}
