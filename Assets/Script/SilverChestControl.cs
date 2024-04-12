using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilverChestControl : MonoBehaviour
{
    [SerializeField] private GameObject interactiveText;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Animator anim;


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactiveText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                anim.SetTrigger("Open");
                StartCoroutine(GetSilverKey());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactiveText.SetActive(false);
        }
    }

    private IEnumerator GetSilverKey()
    {
        yield return new WaitForSeconds(7f);
        Messenger.Broadcast(GameEvent.KEY_PICKUP);
        Destroy(gameObject); // Destroy the chest object
    }
}

