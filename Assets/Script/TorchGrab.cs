using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchGrab : MonoBehaviour
{
    [SerializeField] private GameObject interactiveText;
    [SerializeField] private GameObject torchOnPlayer;
    [SerializeField] private Light torchLight;
    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource grabAudioSource;
    [SerializeField] private GameObject exclamationMark;
    private bool isCoroutineRunning = false;
    void Start()
    {
        interactiveText.SetActive(false);
        torchOnPlayer.SetActive(false);
    }

   
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            interactiveText.SetActive(true);
            if(Input.GetKey(KeyCode.E) && !isCoroutineRunning)
            {
                exclamationMark.SetActive(false);
                Messenger.Broadcast(GameEvent.TORCH_GRAB);
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
        if (!grabAudioSource.isPlaying)
        {
            grabAudioSource.Play();
        }
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

    
}

