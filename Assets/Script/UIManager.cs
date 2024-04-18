using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private ScrollPanelPopup pausePopup;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausePopup.Open();
            Time.timeScale = 0f;
            StartCoroutine(DelayedPauseGame());
        }
    }
   
    public void ReturnToGame()
    {
        Time.timeScale = 1.0f;
        pausePopup.Close();
    }

    IEnumerator DelayedPauseGame()
    {
        yield return new WaitForEndOfFrame(); // Wait until the end of frame
        Messenger.Broadcast(GameEvent.PAUSE_GAME); // Broadcast the event after a frame
    }

}
