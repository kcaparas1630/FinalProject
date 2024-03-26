using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    bool hasTakenDamage = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player got hit!");
            Messenger.Broadcast(GameEvent.PLAYER_HIT);
        }
    }
  
}
