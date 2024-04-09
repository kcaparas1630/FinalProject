using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPopUp : MonoBehaviour
{
    [SerializeField] private AudioSource gameOverSound;
    private void Start()
    {
        this.gameObject.SetActive(false);
    }
    private void Awake()
    {
        Messenger.AddListener(GameEvent.GAME_OVER, OnGameOver);
    }
    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.GAME_OVER, OnGameOver);
    }
    
    private void OnGameOver()
    {
        Debug.Log("Gets inside");
        this.gameObject.SetActive(true);
        if (!gameOverSound.isPlaying)
        {
            gameOverSound.Play();
        }
        StartCoroutine(pauseGame());
    }

    public void RestartGame()
    {
        Debug.Log("Apply restart on Application Build");
    }
    public void ExitGame()
    {
        Debug.Log("Apply Menu exit on Application Build");
    }

    IEnumerator pauseGame()
    {
        yield return new WaitForSeconds(1f);
        // Pause the game
        Time.timeScale = 0f;
    }
}
