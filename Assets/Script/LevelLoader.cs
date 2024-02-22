using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    [SerializeField] Animator transition;
    public static Vector3 playerPosition;
    public float transitionTime = 1f;
    // Reference to the player prefabs
    //[SerializeField] private GameObject playerPrefabNearStairs;
    public GameObject playerPositionPlaceholder;
    [SerializeField] private GameObject originalPlayer;
    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadNextLevel()
    {
        // Store the player's position before transitioning
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        // Check if the LevelLoader GameObject is inactive
        bool wasActive = gameObject.activeSelf;

        // Activate the LevelLoader GameObject temporarily if it's inactive
        if (!wasActive)
        {
            gameObject.SetActive(true);
        }

        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));

        // Deactivate the LevelLoader GameObject again if it was inactive
        if (!wasActive)
        {
            gameObject.SetActive(false);
        }
    }

    public void LoadPreviousLevel()
    {
        // Store the player's position before transitioning
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        // Check if the LevelLoader GameObject is inactive
        bool wasActive = gameObject.activeSelf;

        // Activate the LevelLoader GameObject temporarily if it's inactive
        if (!wasActive)
        {
            gameObject.SetActive(true);
        }

        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex - 1));

        // Deactivate the LevelLoader GameObject again if it was inactive
        if (!wasActive)
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        // Set the player's position near the stairs
        if (playerPositionPlaceholder != null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player.transform.position = playerPositionPlaceholder.transform.position;
            }
            else
            {
                Debug.LogWarning("Player not found!");
            }
        }
        else
        {
            Debug.LogWarning("Player position placeholder is not assigned!");
        }

        SceneManager.LoadScene(levelIndex);
    }
}
