using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchGrab : MonoBehaviour
{
    [SerializeField] private GameObject interactiveText;
    [SerializeField] private GameObject torchOnPlayer;
    [SerializeField] private Light torchLight;
    [SerializeField] private Animator anim;

    private bool isCoroutineRunning = false;
    void Start()
    {
        interactiveText.SetActive(false);
        torchOnPlayer.SetActive(false);
    }

    private void Update()
    {
        // Check for "R" key press to turn off torch light
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

        // Wait for 2 or 3 seconds
        float delay = Random.Range(2f, 3f);
        yield return new WaitForSeconds(delay);

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

