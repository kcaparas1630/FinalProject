using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollPopupFinal : MonoBehaviour
{
    [SerializeField] private GameObject interactiveText;
    [SerializeField] private ScrollPanelPopup scrollPopup;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactiveText.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                scrollPopup.Open();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactiveText.SetActive(false);
        }
    }
}
