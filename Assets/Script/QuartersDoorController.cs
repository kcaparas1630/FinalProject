using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuartersDoorController : MonoBehaviour
{
    [SerializeField] private GameObject interactiveText;
    [SerializeField] private Animator door1Anim;
    [SerializeField] private Animator door2Anim;
    [SerializeField] private Animator playerAnim;
    [SerializeField] private AudioSource doorOpen;
    private bool isOpen = false;
    //Start is called before the first frame update
    void Start()
    {
        interactiveText.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            interactiveText.SetActive(true);
            if (Input.GetKey(KeyCode.E) && !isOpen)
            {
                Debug.Log("Open");
                playerAnim.SetTrigger("OpenDoor");
                door1Anim.SetBool("Open", true);
                door2Anim.SetBool("Open", true);
                isOpen = true;
                if (!doorOpen.isPlaying)
                {
                    doorOpen.Play();
                }
            }
            if (Input.GetKey(KeyCode.E) && isOpen)
            {
                playerAnim.SetTrigger("OpenDoor");
                door1Anim.SetBool("Open", false);
                door2Anim.SetBool("Open", false);
                isOpen = false;
                if (!doorOpen.isPlaying)
                {
                    doorOpen.Play();
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

}
