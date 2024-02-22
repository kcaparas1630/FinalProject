using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : MonoBehaviour
{
    [SerializeField] private GameObject interactiveText;
    [SerializeField] private GameObject keyText;
    [SerializeField] private GameObject itemObject;
    // Start is called before the first frame update
    void Start()
    {
        interactiveText.SetActive(false);
        keyText.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            interactiveText.SetActive(true);
            if (this.gameObject.CompareTag("Chest"))
            {
                keyText.SetActive(true);
            }
            else
            {
                if (Input.GetKey(KeyCode.E))
                {
                    itemObject.SetActive(false);
                    interactiveText.SetActive(false);
                    keyText.SetActive(false);
                    //add implementation with inventory soon. For now just hide it.
                    // check condition if chest,

                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        interactiveText.SetActive(false);
        keyText.SetActive(false);
    }
}
