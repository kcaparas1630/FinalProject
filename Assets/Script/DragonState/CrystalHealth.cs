using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalHealth : MonoBehaviour
{
    private int collideCount = 0;
    private bool hasHit = false;
    [SerializeField] private GameObject crystal;
    [SerializeField] private AudioSource crystalHit;
    private bool isDestroying = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerSword") && !isDestroying)
        {
            StartCoroutine(collideCounter());
        }
    }

    IEnumerator collideCounter()
    {
        //set to true to prevent multi-hit
        hasHit = true;
        if (hasHit)
        {
            collideCount++;
            crystalHit.Play();
            Debug.Log(collideCount);

            if (collideCount >= 3)
            {
                isDestroying = true;
                StartCoroutine(DestroyCrystal());
            }
        }
        yield return new WaitForSeconds(3f);
        //return to false to cycle hasHit
        hasHit = false;
    }

    IEnumerator DestroyCrystal()
    {
        Messenger.Broadcast(GameEvent.BOSSHEALTH_REDUCE);
        yield return new WaitForSeconds(3f);
        Destroy(crystal);
    }



}
