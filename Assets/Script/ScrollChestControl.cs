using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollChestControl : MonoBehaviour
{
    [SerializeField] private GameObject interactiveText;
    [SerializeField] private GameObject keyText;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private ScrollPanelPopup scrollPopup;
    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource openChest;
    private bool used = false;// flag to prevent OnTriggerStay getting called every frames per second
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            interactiveText.SetActive(true);
            if (!gameManager.SKeyValidation()) {
                keyText.SetActive(true); }
            else { 
                keyText.SetActive(false);
                if (Input.GetKey(KeyCode.E) && !used)
                {
                    used = true;// set used to true to prevent multi use of keys
                    Messenger.Broadcast(GameEvent.OPEN_SCROLLCHEST);
                    anim.SetTrigger("Open");
                    openChest.Play();
                    StartCoroutine(GetScroll());
                }
            }
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            interactiveText.SetActive(false);
            keyText.SetActive(false);
        }
    }

    private IEnumerator GetScroll()
    {
        yield return new WaitForSeconds(7f);
        scrollPopup.Open();
        Destroy(this.gameObject);
    }
}
