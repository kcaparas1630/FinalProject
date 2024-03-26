using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSound : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundSound;
    private bool isCutscenePlaying = false;
    void Start()
    {
        if (!isCutscenePlaying)
        {
            backgroundSound.Play();
        }
        else
        {
            OnCutscenePlaying();
        }
    }
    void Awake()
    {
        Messenger.AddListener(GameEvent.CUTSCENE_PLAYING, OnCutscenePlaying);
        Messenger.AddListener(GameEvent.PLAYER_INJURED, InjuredSound);
    }
    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.CUTSCENE_PLAYING, OnCutscenePlaying);
        Messenger.RemoveListener(GameEvent.PLAYER_INJURED, InjuredSound);
    }
    private void InjuredSound()
    {
        backgroundSound.volume = 0.4f;
    }
    private void OnCutscenePlaying()
    {
        isCutscenePlaying = true;
        backgroundSound.Stop();
    }
   
}
