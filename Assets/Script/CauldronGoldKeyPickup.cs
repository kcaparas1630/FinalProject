using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauldronGoldKeyPickup : MonoBehaviour
{
    [SerializeField] private GameObject interactiveText;
    [SerializeField] private Animator cauldronAnim;
    private bool used = false; // flag to prevent multi trigger from OnTriggerStay
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           interactiveText.SetActive(true);
            if (Input.GetKey(KeyCode.E) && !used)
            {
                cauldronAnim.SetTrigger("Pickup");
                //sets to true to prevent multi-click
                used = true;
                StartCoroutine(GetGoldenKey());
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            interactiveText.SetActive(false);
        }
    }

    IEnumerator GetGoldenKey()
    {
        yield return new WaitForSeconds(7f);
        Messenger.Broadcast(GameEvent.GOLDKEY_PICKUP);
        Messenger.Broadcast(GameEvent.FINAL_EVENT);
    }
}
