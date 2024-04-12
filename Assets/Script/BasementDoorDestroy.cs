using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasementDoorDestroy : MonoBehaviour
{
    private void Awake()
    {
        Messenger.AddListener(GameEvent.DESTROY_BASEMENTDOOR, OnDestroyBasementDoor);
    }
    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.DESTROY_BASEMENTDOOR, OnDestroyBasementDoor);
    }

    private void OnDestroyBasementDoor()
    {
        Destroy(this.gameObject);
    }
}
