using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookInteract : MonoBehaviour
{
    [SerializeField] private GameObject bookCanvas;
    [SerializeField] private GameObject interactiveText;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject exclamationMarkObject;

    private bool eKeyPressed = false;
    // Start is called before the first frame update
    void Start()
    {
        bookCanvas.SetActive(false);
        interactiveText.SetActive(false);

    }

    public void CloseCanvas()
    {
        bookCanvas.SetActive(false);
        eKeyPressed = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            interactiveText.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E) && !eKeyPressed)
            {
                eKeyPressed = true; // Set the flag to true
                bookCanvas.SetActive(true);
                interactiveText.SetActive(false);

                
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (gameManager != null && exclamationMarkObject != null)
                {
                    gameManager.CompleteTask(exclamationMarkObject);
                }
                CloseCanvas();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        interactiveText.SetActive(false);
    }
}
