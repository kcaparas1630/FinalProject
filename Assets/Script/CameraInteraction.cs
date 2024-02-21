using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CameraInteraction : MonoBehaviour
{
    public GameObject interactionText;
    public float interactionDistance = 3f;
    public GameObject dungeonObject;
    [SerializeField] private Animator animator;
    private void Update()
    {
        // Cast a ray forward from the camera
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // Check if the ray hits an interactable object
        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (hit.collider.gameObject.tag == "book")
                {
                    interactionText.SetActive(true);
                    dungeonObject.SetActive(true);
                }
                else if (hit.collider.gameObject.tag == "door")
                {
      
                    animator.SetBool("Open",true);
                    interactionText.SetActive(true);
                    // Additional logic for the door interaction, e.g., triggering animation
                }
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                dungeonObject.SetActive(false);
            }
        }
        else
        {
            // No hit, deactivate UI elements
            interactionText.SetActive(false);
        }
    }
}

