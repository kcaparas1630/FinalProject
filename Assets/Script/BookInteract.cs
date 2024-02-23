using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookInteract : MonoBehaviour
{
    [SerializeField] private GameObject bookCanvas;
    [SerializeField] private GameObject interactiveText;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        bookCanvas.SetActive(false);
        interactiveText.SetActive(false);
    }
    public void closeCanvas()
    {
        bookCanvas.SetActive(false);
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            interactiveText.SetActive(true);
            if(Input.GetKey(KeyCode.E))
            {   
                bookCanvas.SetActive(true);
                interactiveText.SetActive(false);
                
                if (gameManager != null)
                {
                    // Assuming Exclamation_Yellow is a direct child of the book object
                    Transform exclamationMark = transform.Find("Exclamation_Yellow");
                    
                    if (exclamationMark != null)
                    {
                        
                        gameManager.CompleteTask(exclamationMark.gameObject);
                    }
                    else
                    {
                        Debug.LogWarning("Exclamation_Yellow not found as a child of the book!");
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        interactiveText.SetActive(false);
    }
}
