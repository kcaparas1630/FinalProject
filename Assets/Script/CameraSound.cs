using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSound : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundSound;
    [SerializeField] private AudioClip bossFight;
    void Start()
    {
        
    }
    void Awake()
    {
        Messenger.AddListener(GameEvent.CUTSCENE_PLAYING, OnCutscenePlaying);
        Messenger.AddListener(GameEvent.CUTSCENE_FINISHED, OnCutsceneFinished);
        Messenger.AddListener(GameEvent.PLAYER_INJURED, InjuredSound);
        Messenger.AddListener(GameEvent.GAME_OVER, OnGameOver);
        Messenger.AddListener(GameEvent.BOSS_CUTSCENE_FINISHED, OnBossCutsceneFinished);
        Messenger.AddListener(GameEvent.BOSS_CUTSCENE_PLAYING, OnBossCutscenePlaying);
        Messenger.AddListener(GameEvent.GAME_START_CUTSCENE_PLAYING, OnCutscenePlaying);
        Messenger.AddListener(GameEvent.GAME_START_CUTSCENE_FINISHED, OnCutsceneFinished);
    }
    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.CUTSCENE_PLAYING, OnCutscenePlaying);
        Messenger.RemoveListener(GameEvent.CUTSCENE_FINISHED, OnCutsceneFinished);
        Messenger.RemoveListener(GameEvent.PLAYER_INJURED, InjuredSound);
        Messenger.RemoveListener(GameEvent.GAME_OVER, OnGameOver);
        Messenger.RemoveListener(GameEvent.BOSS_CUTSCENE_FINISHED, OnBossCutsceneFinished);
        Messenger.RemoveListener(GameEvent.BOSS_CUTSCENE_PLAYING, OnBossCutscenePlaying);
        Messenger.RemoveListener(GameEvent.GAME_START_CUTSCENE_PLAYING, OnCutscenePlaying);
        Messenger.RemoveListener(GameEvent.GAME_START_CUTSCENE_FINISHED, OnCutsceneFinished);
    }
    private void OnBossCutscenePlaying()
    {
        backgroundSound.Stop();
    }
    private void OnBossCutsceneFinished()
    {
        //change camera sound to bossfight sound
        backgroundSound.clip = bossFight;
        backgroundSound.Play();
    }
    private void OnGameOver()
    {
        //stop sound because another audio source will play, on a different class
        backgroundSound.Stop();
    }
    private void InjuredSound()
    {
        backgroundSound.volume = 0.4f;
    }
    private void OnCutscenePlaying()
    {
        backgroundSound.Stop();
    }
    private void OnCutsceneFinished()
    {
        backgroundSound.Play();
    }
   
}
