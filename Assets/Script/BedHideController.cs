using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedHideController : MonoBehaviour
{
    [SerializeField] private GameObject playerModel;
    [SerializeField] private Animator playerAnim;
    [SerializeField] private GameObject interactiveText;
    [SerializeField] private GameObject torch;
    private bool isUnderBed = false; // flag to prevent multi click
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactiveText.SetActive(true);
            //checks if player is not under bed.
            if (Input.GetKeyDown(KeyCode.E) && !isUnderBed)
            {
                playerAnim.SetTrigger("Hide");
                StartCoroutine(finishHideAnimation());
                interactiveText.SetActive(false);
                //sends broadcast to enemy class to trigger patrol state
                Messenger.Broadcast(GameEvent.UNDER_BED);
            }
            //checks if player is under bed
            else if(Input.GetKeyDown(KeyCode.E) && isUnderBed)
            {
                playerAnim.SetTrigger("Unhide");
                playerModel.SetActive(true);
                torch.SetActive(true);
                StartCoroutine(cooldownHideAnimation());
                interactiveText.SetActive(true);
                //sends broadcast to enemy class to trigger either chase or attack
                Messenger.Broadcast(GameEvent.EXIT_BED);
            }
        }
    }
    IEnumerator finishHideAnimation()
    {
        yield return new WaitForSeconds(1.5f);
        //when hidden hide, playerModel and torch
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
