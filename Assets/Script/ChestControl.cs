using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChestControl : MonoBehaviour
{
    [SerializeField] private GameObject interactiveText;
    [SerializeField] private GameObject keyText;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource openChest;
    private bool used = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactiveText.SetActive(true);

            // Check if the player has a silver key
            if (!gameManager.SKeyValidation())
            {
                keyText.SetActive(true);
            }
            else
            {
                keyText.SetActive(false);
                // If the player presses E and hasn't used the chest yet
                if (Input.GetKeyDown(KeyCode.E) && !used)
                {
                    used = true;
                    keyText.SetActive(false);
                    interactiveText.SetActive(false);
                    Messenger.Broadcast(GameEvent.OPEN_CHEST);
                    openChest.Play();
                    anim.SetTrigger("Open");
                    StartCoroutine(GetGoldenKey());
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactiveText.SetActive(false);
            keyText.SetActive(false);
        }
    }

    private IEnumerator GetGoldenKey()
    {
        yield return new WaitForSeconds(7f);
        Messenger.Broadcast(GameEvent.GOLDKEY_PICKUP);
        Destroy(gameObject); // Destroy the chest object
    }
}


