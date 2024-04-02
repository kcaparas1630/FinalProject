using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
public class DoorController : MonoBehaviour
{
    [SerializeField] private GameObject interactiveText;
    [SerializeField] private GameObject doorBudgeText;
    [SerializeField] private Animator anim;
    [SerializeField] private Animator playerAnim;
    [SerializeField] private AudioSource doorOpen;
    private bool isOpen = false;
    private void Start()
    {
        interactiveText.SetActive(false);
        doorBudgeText.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            interactiveText.SetActive(true);
            if (this.gameObject.CompareTag("GateFinish"))
            {
                if (Input.GetKey(KeyCode.E))
                {
                    //Change this into coroutine and grab a open door animation.
                    doorBudgeText.SetActive(true);
                    if(!doorOpen.isPlaying)
                    {
                        doorOpen.Play();
                    }
                }
            }
            if (Input.GetKey(KeyCode.E))
            {
                if(!isOpen)
                {
                    playerAnim.SetTrigger("OpenDoor");
                    anim.SetBool("Open", true);
                    StartCoroutine(DoorWaitOpen());
                    if (!doorOpen.isPlaying)
                    {
                        doorOpen.Play();
                    }
                }
                else
                {
                    playerAnim.SetTrigger("OpenDoor");
                    anim.SetBool("Open", false);
                    StartCoroutine(DoorWaitClose());
                    if (!doorOpen.isPlaying)
                    {
                        doorOpen.Play();
                    }
                }
                
                
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            interactiveText.SetActive(false);
        }
    }

    private IEnumerator DoorWaitOpen()
    {
        yield return new WaitForSeconds(2);
        isOpen = true;
    }
    private IEnumerator DoorWaitClose()
    {
        yield return new WaitForSeconds(2);
        isOpen = false;
    }
}
