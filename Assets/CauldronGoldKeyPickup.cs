using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauldronGoldKeyPickup : MonoBehaviour
{
    [SerializeField] private GameObject interactiveText;
    [SerializeField] private Animator cauldronAnim;
    private bool used = false;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           interactiveText.SetActive(true);
            if (Input.GetKey(KeyCode.E) && !used)
            {
                cauldronAnim.SetTrigger("Pickup");
                used = true;
                StartCoroutine(GetGoldenKey());
            }
        }
    }

    IEnumerator GetGoldenKey()
    {
        yield return new WaitForSeconds(7f);
        Messenger.Broadcast(GameEvent.GOLDKEY_PICKUP);
    }
}
