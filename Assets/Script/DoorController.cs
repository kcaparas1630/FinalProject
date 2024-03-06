using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
public class DoorController : MonoBehaviour
{
    [SerializeField] private GameObject interactiveText;
    [SerializeField] private GameObject door;
    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource doorOpen;
    private void Start()
    {
        interactiveText.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            interactiveText.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                anim.SetBool("Open", true);
                if(!doorOpen.isPlaying)
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
