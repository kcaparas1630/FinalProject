using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsLevel : MonoBehaviour
{
    [SerializeField] private GameObject interactiveText;
    [SerializeField] private GameObject floorText;
    [SerializeField] private GameObject level;
    LevelLoader levelLoader;
    void Start()
    {
        levelLoader = level.GetComponent<LevelLoader>();
        floorText.SetActive(false);
        interactiveText.SetActive(false);
        level.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            floorText.SetActive(true);
            interactiveText.SetActive(true); 
            if(Input.GetKey(KeyCode.E))
            {
                level.SetActive(true);
                levelLoader.LoadNextLevel();
                floorText.SetActive(false);
                interactiveText.SetActive(false);
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        floorText.SetActive(false);
        interactiveText.SetActive(false);
    }
}
