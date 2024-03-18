using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterChest : MonoBehaviour
{
    [SerializeField] private GameObject interactiveText;
    [SerializeField] private GameObject keyText;
    [SerializeField] private UpdateUIManager ui;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject enemy;
   
    private bool used = false;
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            interactiveText.SetActive(true);
            if (ui.GetKeyCount() < 0) { keyText.SetActive(true); }
            else { keyText.SetActive(false); }
            if (Input.GetKey(KeyCode.E) && !used)
            {
                used = true;
                Messenger.Broadcast(GameEvent.OPEN_MONSTERCHEST);
                anim.SetTrigger("Open");
                StartCoroutine(MonsterInstantiate());
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            interactiveText.SetActive(false);
        }
    }
    private IEnumerator MonsterInstantiate()
    {
        yield return new WaitForSeconds(5f);
        enemy.SetActive(true);
    }
}
