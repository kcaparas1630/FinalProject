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
                interactiveText.SetActive(false);
                playerAnim.SetTrigger("OpenDoor");
                door1Anim.SetBool("Open", true);
                door2Anim.SetBool("Open", true);
                if (!doorOpen.isPlaying)
                {
                    doorOpen.Play();
                }
            }
        }
    }
}
