using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameStartTimelineController : MonoBehaviour
{
    [SerializeField] private PlayableDirector timeline;
    private bool cutscenePlaying = false;
    void Start()
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
        Destroy(this.gameObject);
    }
}
