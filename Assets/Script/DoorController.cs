using System.Collections;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        // Assuming the Animator is on the same GameObject as this script
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Open");
            animator.SetBool("OpenDoor",true);
        }
    }

}
