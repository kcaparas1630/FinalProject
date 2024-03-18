using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChestControl : MonoBehaviour
{
    [SerializeField] private GameObject interactiveText;
    [SerializeField] private GameObject keyText;
    [SerializeField] private UpdateUIManager ui;
    [SerializeField] private Animator anim;
    private bool used = false;
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            interactiveText.SetActive(true);
            if(ui.GetKeyCount() < 0){ keyText.SetActive(true); }
            else { keyText.SetActive(false); }
            if (Input.GetKey(KeyCode.E) && !used)
            {
                used = true;
                Messenger.Broadcast(GameEvent.OPEN_CHEST);
                anim.SetTrigger("Open");
                StartCoroutine(GetGoldenKey());
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

    private IEnumerator GetGoldenKey()
    {
        yield return new WaitForSeconds(7f);
        Messenger.Broadcast(GameEvent.GOLDKEY_PICKUP);
        Destroy(this.gameObject);
    }
}
