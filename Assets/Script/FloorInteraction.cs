using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class FloorInteraction : MonoBehaviour
{
    [SerializeField] private GameObject floorText;
    [SerializeField] private GameObject placeholderFloor;
    // Start is called before the first frame update
    void Start()
    {
        floorText.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            floorText.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        floorText.SetActive(false);
    }
}
