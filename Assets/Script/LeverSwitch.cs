using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class LeverSwitch : MonoBehaviour
{
    [SerializeField] private GameObject interactiveText;
    [SerializeField] private Animator leverAnim;
    [SerializeField] private PlayableDirector timeline;
    [SerializeField] private Animator playerAnim;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactiveText.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                leverAnim.SetTrigger("Down");
                playerAnim.SetTrigger("OpenDoor");
                interactiveText.SetActive(false);
                StartCoroutine(leverTimelineStart());
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactiveText.SetActive(false);
        }
    }

    IEnumerator leverTimelineStart()
    {
        yield return new WaitForSeconds(2f);
        timeline.Play();
    }
}
