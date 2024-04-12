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
        hasHit = true;
        yield return new WaitForSeconds(3f);

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

        hasHit = false;
    }

    IEnumerator DestroyCrystal()
    {
        Messenger.Broadcast(GameEvent.BOSSHEALTH_REDUCE);
        yield return new WaitForSeconds(3f);
        Destroy(crystal);
    }



}
