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
                playerAnim.SetTrigger("OpenDoor");
                anim.SetBool("Open", true);
                if (!doorOpen.isPlaying)
                {
                    doorOpen.Play();
                }
                interactiveText.SetActive(false);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            anim.SetBool("Open", false);
           
            if (!doorOpen.isPlaying)
            {
                doorOpen.Play();
            }
            interactiveText.SetActive(false);
        }
    }
}
