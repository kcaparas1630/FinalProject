using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookInteract : MonoBehaviour
{
    [SerializeField] private GameObject bookCanvas;
    [SerializeField] private GameObject interactiveText;
    // Start is called before the first frame update
    void Start()
    {
        bookCanvas.SetActive(false);
        interactiveText.SetActive(false);

    }

    public void CloseCanvas()
    {
        bookCanvas.SetActive(false);
    }
    public void Pause()
    {
        Time.timeScale = 0;
    }
    public void Continue()
    {
        Time.timeScale = 1;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            interactiveText.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                bookCanvas.SetActive(true);
                interactiveText.SetActive(false);
                //Pause(); Find a way to pause but continue canvas
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        interactiveText.SetActive(false);
    }
}
