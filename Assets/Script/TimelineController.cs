using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
public class TimelineController : MonoBehaviour
{
    [SerializeField] private PlayableDirector timeline;
    private bool cutscenePlaying = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Change the tag as per your requirements
        {
            // Play the timeline when the player enters the collider
            timeline.Play();
            Messenger.Broadcast(GameEvent.CUTSCENE_PLAYING);
            cutscenePlaying = true;
        }
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
        // For example, you can resume player control or set the player's position

        // Reset the cutscene playing flag
        cutscenePlaying = false;
    }
}
