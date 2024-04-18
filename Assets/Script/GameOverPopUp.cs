using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPopUp : MonoBehaviour
{
    [SerializeField] private AudioSource gameOverSound;
    [SerializeField] private AudioClip gameFinishedSound;
    private void Start()
    {
        this.gameObject.SetActive(false);
    }
    private void Awake()
    {
        Messenger.AddListener(GameEvent.GAME_OVER, OnGameOver);
        Messenger.AddListener(GameEvent.GAME_FINISHED, OnGameFinished);
    }
    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.GAME_OVER, OnGameOver);
        Messenger.RemoveListener(GameEvent.GAME_FINISHED, OnGameFinished);
    }
    
    private void OnGameFinished()
    {
        //sets the ui panel to active
        this.gameObject.SetActive(true);
        if(!gameOverSound.isPlaying)
        {
            //change gameOverSound to a happy melody
            gameOverSound.clip = gameFinishedSound;
            gameOverSound.Play();
        }
    }
    private void OnGameOver()
    {
        //sets the ui panel to active
        this.gameObject.SetActive(true);
        if (!gameOverSound.isPlaying)
        {
            gameOverSound.Play();
        }
        StartCoroutine(pauseGame());
    }
    IEnumerator pauseGame()
    {
        yield return new WaitForSeconds(1f);
        // Pause the game
        Time.timeScale = 0f;
    }
}
