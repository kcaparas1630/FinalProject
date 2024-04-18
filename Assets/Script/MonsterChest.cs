using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterChest : MonoBehaviour
{
    [SerializeField] private GameObject interactiveText;
    [SerializeField] private GameObject keyText;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject enemy;
    [SerializeField] private AudioSource openChest;

    private bool used = false;// flag to prevent OnTriggerStay getting called every frames per second
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            interactiveText.SetActive(true);
            if (!gameManager.SKeyValidation()) { keyText.SetActive(true); }
            else { 
                keyText.SetActive(false);
                if (Input.GetKey(KeyCode.E) && !used)
                {
                    used = true; // set used to true to prevent multi use of keys
                    Messenger.Broadcast(GameEvent.OPEN_MONSTERCHEST);
                    anim.SetTrigger("Open");
                    openChest.Play();
                    StartCoroutine(MonsterInstantiate());
                }
            }
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            interactiveText.SetActive(false);
            keyText.SetActive(false);
        }
    }
    private IEnumerator MonsterInstantiate()
    {
        yield return new WaitForSeconds(5f);
        enemy.SetActive(true);
    }
}
