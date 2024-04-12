using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCircleController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(destroyCircle());
        }
    }
    IEnumerator destroyCircle()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
}
