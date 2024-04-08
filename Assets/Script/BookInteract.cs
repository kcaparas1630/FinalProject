using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookInteract : MonoBehaviour
{
    [SerializeField] private GameObject bookCanvas;
    [SerializeField] private GameObject interactiveText;
    [SerializeField] private GameObject exclamationMark;
    private bool isTaskCompleted = false;
    // Start is called before the first frame update
    void Start()
    {
        bookCanvas.SetActive(false);
        interactiveText.SetActive(false);

    }

    public void CloseCanvas()
    {
        exclamationMark.SetActive(false);
        bookCanvas.SetActive(false);
        if (!isTaskCompleted) // Check if the task is not already completed
        {
            isTaskCompleted = true; // Set the task as completed
            Messenger.Broadcast(GameEvent.CLOSE_CANVAS);
        }
    }
    public void Pause()
    {
        Time.timeScale = 0;
    }
    public void Continue()
    {
        Time.timeScale = 1;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            interactiveText.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                bookCanvas.SetActive(true);
                interactiveText.SetActive(false);
                Messenger.Broadcast(GameEvent.OPEN_CANVAS);
                //Pause(); Find a way to pause but continue canvas
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        interactiveText.SetActive(false);
    }

}
