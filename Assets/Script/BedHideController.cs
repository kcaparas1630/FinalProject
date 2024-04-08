using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedHideController : MonoBehaviour
{
    [SerializeField] private GameObject playerModel;
    [SerializeField] private Animator playerAnim;
    [SerializeField] private GameObject interactiveText;
    [SerializeField] private GameObject torch;
    private bool isUnderBed = false;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactiveText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E) && !isUnderBed)
            {
                playerAnim.SetTrigger("Hide");
                StartCoroutine(finishHideAnimation());
                interactiveText.SetActive(false);
                Messenger.Broadcast(GameEvent.UNDER_BED);
            }
            else if(Input.GetKeyDown(KeyCode.E) && isUnderBed)
            {
                playerAnim.SetTrigger("Unhide");
                playerModel.SetActive(true);
                torch.SetActive(true);
                StartCoroutine(cooldownHideAnimation());
                interactiveText.SetActive(true);
                Messenger.Broadcast(GameEvent.EXIT_BED);
            }
        }
    }
    IEnumerator finishHideAnimation()
    {
        yield return new WaitForSeconds(1.5f);
        playerModel.SetActive(false);
        torch.SetActive(false);
        isUnderBed = true;
    }
    IEnumerator cooldownHideAnimation()
    {
        yield return new WaitForSeconds(1.5f);
        isUnderBed = false;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactiveText.SetActive(true);
        }
    }
}
