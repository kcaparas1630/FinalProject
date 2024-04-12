using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabSword : MonoBehaviour
{
    [SerializeField] private GameObject interactiveText;
    [SerializeField] private GameObject swordOnPlayer;
    [SerializeField] private Animator anim;
    private bool isCoroutineRunning = false;
    void Start()
    {
        interactiveText.SetActive(false);
        swordOnPlayer.SetActive(false);
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            interactiveText.SetActive(true);
            if (Input.GetKey(KeyCode.E) && !isCoroutineRunning)
            {
                StartCoroutine(GrabSwordWithDelay());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        interactiveText.SetActive(false);
    }

    private IEnumerator GrabSwordWithDelay()
    {
        isCoroutineRunning = true;

        anim.SetTrigger("Pickup");
        //wait for 2 seconds
        yield return new WaitForSeconds(1.8f);

        // Deactivate this torch and activate the torch on the player
        this.gameObject.SetActive(false);
        swordOnPlayer.SetActive(true);

        // Hide the interactive text
        interactiveText.SetActive(false);

        isCoroutineRunning = false;
    }
}
