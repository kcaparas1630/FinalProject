using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    [SerializeField] private GameObject interactiveText;
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            interactiveText.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                Debug.Log("clicked!");
                Messenger.Broadcast(GameEvent.KEY_PICKUP);
                this.gameObject.SetActive(false);
                interactiveText.SetActive(false);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            interactiveText.SetActive(false);
        }
    }
}
