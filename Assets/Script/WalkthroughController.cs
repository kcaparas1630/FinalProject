using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkthroughController : MonoBehaviour
{
    [SerializeField] private GameObject movementKeyText;
    [SerializeField] private GameObject interactionKeyText;
    [SerializeField] private GameObject torchWaveKeyText;
    [SerializeField] private GameObject followText;
    [SerializeField] private GameObject pauseText;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private List<GameObject> walkthroughCircles = new List<GameObject>();
    [SerializeField] private CinemachineVirtualCamera basementVirtualCamera;
    private void Start()
    {
        movementKeyText.SetActive(false);
        
    }
    private void Awake()
    {
        Debug.Log("WalkthroughController Awake");
        Messenger.AddListener(GameEvent.OPEN_CANVAS, OnOpenCanvas);
        Messenger.AddListener(GameEvent.WALKTHROUGH_CIRCLE, OnWalkthroughCircle);
        Messenger.AddListener(GameEvent.TORCH_GRAB, OnTorchGrab);
        Messenger.AddListener(GameEvent.TORCH_WAVE, OnTorchWave);
        Messenger.AddListener(GameEvent.PAUSE_GAME, OnPauseGame);
        Messenger.AddListener(GameEvent.GAME_START_CUTSCENE_FINISHED, OnCutsceneFinished);
    }
    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.OPEN_CANVAS, OnOpenCanvas);
        Messenger.RemoveListener(GameEvent.WALKTHROUGH_CIRCLE, OnWalkthroughCircle);
        Messenger.RemoveListener(GameEvent.TORCH_GRAB, OnTorchGrab);
        Messenger.RemoveListener(GameEvent.TORCH_WAVE, OnTorchWave);
        Messenger.RemoveListener(GameEvent.PAUSE_GAME, OnPauseGame);
        Messenger.RemoveListener(GameEvent.GAME_START_CUTSCENE_FINISHED, OnCutsceneFinished);
    }

    private void OnCutsceneFinished()
    {
        StartCoroutine(textShow());
    }
    public void OnPauseGame()
    {
        pauseText.SetActive(false);
        StartCoroutine(broadcastDoor());
        StartCoroutine(destroyGameObject());
        basementVirtualCamera.enabled = false; // brute force disable
    }
    private void OnTorchWave()
    {
        torchWaveKeyText.SetActive(false);
        pauseText.SetActive(true);
    }
    private void OnTorchGrab()
    {
        followText.SetActive(false);
        torchWaveKeyText.SetActive(true);
    }
    private void OnWalkthroughCircle()
    {
        movementKeyText.SetActive(false);
        interactionKeyText.SetActive(true);
    }
    private void OnOpenCanvas()
    {
        interactionKeyText.SetActive(false);
        // Activate all walkthrough circles
        foreach (GameObject circle in walkthroughCircles)
        {
            circle.SetActive(true);
        }
        followText.SetActive(true);
    }
  
    IEnumerator textShow()
    {
        yield return new WaitForSeconds(3.5f);
        movementKeyText.SetActive(true);

    }
    IEnumerator broadcastDoor()
    {
        yield return new WaitForSeconds(2f);
        Messenger.Broadcast(GameEvent.BASEMENTDOOR_OPEN);
    }
    IEnumerator destroyGameObject()
    {
        yield return new WaitForSeconds(1.3F);
        Destroy(this.gameObject);
    }
  
}
