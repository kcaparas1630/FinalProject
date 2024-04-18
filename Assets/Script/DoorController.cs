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
    private bool isOpen = false;// flag to trigger different animations
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
            if (Input.GetKey(KeyCode.E))
            {
                //if not open then trigger open animation
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
                //if open trigger close animation
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
