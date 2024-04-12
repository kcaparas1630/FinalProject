using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalAppear : MonoBehaviour
{
   private void OnAwake()
    {
        Messenger.AddListener(GameEvent.CRYSTAL_APPEAR, onCrystalAppear);
    }
    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.CRYSTAL_APPEAR, onCrystalAppear);
    }

    private void onCrystalAppear()
    {
        this.gameObject.SetActive(true);
    }
}
