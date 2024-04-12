using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class DoorTimelineController : MonoBehaviour
{
    [SerializeField] private PlayableDirector timeline;
    [SerializeField] private TextMeshProUGUI gateOpen;
    private bool cutscenePlaying = false;
    private void Awake()
    {
        Messenger.AddListener(GameEvent.BASEMENTDOOR_OPEN, openDoor);
    }
    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.BASEMENTDOOR_OPEN, openDoor);
    }

    private void openDoor()
    {
        timeline.Play();
        Messenger.Broadcast(GameEvent.CUTSCENE_PLAYING);
        cutscenePlaying = true;
    }
    private void Update()
    {
        // Check if the cutscene is playing and if the timeline is finished
        if (cutscenePlaying && timeline.state != PlayState.Playing)
        {
            // Trigger an event or perform any action when the cutscene finishes
            OnCutsceneFinished();
        }
    }

    private void OnCutsceneFinished()
    {
        // Add your logic here to handle the end of the cutscene
        Debug.Log("Cutscene finished!");
        Messenger.Broadcast(GameEvent.CUTSCENE_FINISHED);
        // Reset the cutscene playing flag
        cutscenePlaying = false;
        gateOpen.enabled = false;
    }
}
