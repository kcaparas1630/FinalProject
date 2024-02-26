using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchGrab : MonoBehaviour
{
    [SerializeField] private GameObject interactiveText;
    [SerializeField] private GameObject torchOnPlayer;
    [SerializeField] private Light torchLight;
    [SerializeField] private Animator anim;
    private GameManager gameManager;

    private bool isCoroutineRunning = false;
    void Start()
    {
        interactiveText.SetActive(false);
        torchOnPlayer.SetActive(false);
        gameManager = GameManager.Instance;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            TurnOffTorch();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            interactiveText.SetActive(true);
            if (Input.GetKey(KeyCode.E) && !isCoroutineRunning)
            {
                StartCoroutine(GrabTorchWithDelay());
                if (gameManager != null)
                {
                    Transform exclamationMark = transform.Find("Exclamation_Yellow");

                    if (exclamationMark != null)
                    {

                        gameManager.CompleteTask(exclamationMark.gameObject);
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        interactiveText.SetActive(false);
    }

    private IEnumerator GrabTorchWithDelay()
    {
        isCoroutineRunning = true;

        // Play the "Grab" animation
        anim.SetTrigger("Grab");
        //wait for 2 seconds
        yield return new WaitForSeconds(1.8f);

        // Deactivate this torch and activate the torch on the player
        this.gameObject.SetActive(false);
        torchOnPlayer.SetActive(true);
        
        // Hide the interactive text
        interactiveText.SetActive(false);

        isCoroutineRunning = false;
    }

    private void TurnOffTorch()
    {
        // Turn off the torch light on the player
        torchOnPlayer.SetActive(false);
    }
}

