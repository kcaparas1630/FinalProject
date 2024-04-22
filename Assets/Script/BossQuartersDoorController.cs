using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossQuartersDoorController : MonoBehaviour
{
    [SerializeField] private GameObject interactiveText;
    [SerializeField] private Animator door1Anim;
    [SerializeField] private Animator door2Anim;
    [SerializeField] private Animator playerAnim;
    [SerializeField] private AudioSource doorOpen;
    private bool isOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        interactiveText.SetActive(false);

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            interactiveText.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                if(!isOpen)
                {
                    interactiveText.SetActive(false);
                    playerAnim.SetTrigger("OpenDoor");
                    door1Anim.SetBool("Open", true);
                    door2Anim.SetBool("Open", true);
                    StartCoroutine(DoorWaitOpen());
                    if (!doorOpen.isPlaying)
                    {
                        doorOpen.Play();
                    }
                }
                else
                {
                    interactiveText.SetActive(false);
                    playerAnim.SetTrigger("OpenDoor");
                    door1Anim.SetBool("Open", false);
                    door2Anim.SetBool("Open", false);
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
        if(other.gameObject.tag == "Player")
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
