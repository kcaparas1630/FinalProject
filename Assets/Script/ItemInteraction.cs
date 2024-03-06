using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : MonoBehaviour
{
    [SerializeField] private GameObject interactiveText;
    [SerializeField] private GameObject keyText;
    [SerializeField] private GameObject itemObject;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject monster;
    // Start is called before the first frame update
    void Start()
    {
        interactiveText.SetActive(false);
        keyText.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            interactiveText.SetActive(true);
            if (this.gameObject.CompareTag("GChest"))
            {
                keyText.SetActive(true);
                if(Input.GetKey(KeyCode.E))
                {
                    
                    //itemObject.SetActive(false);
                    if (gameManager.SKeyValidation())
                    {
                        gameManager.UseSKey();
                        anim.SetTrigger("Open");
                        StartCoroutine(GetGoldenKey());
                    }
                    
                }
            }
            else if (this.gameObject.CompareTag("ScrollChest"))
            {
                keyText.SetActive(true);
                if (Input.GetKey(KeyCode.E))
                {
                    if (gameManager.SKeyValidation())
                    {
                        gameManager.UseSKey();
                        anim.SetTrigger("Open");
                        keyText.SetActive(false);
                        StartCoroutine(GetScroll());
                    }
                }
            }
            else if (this.gameObject.CompareTag("FakeChest"))
            {
                keyText.SetActive(true);
                if (Input.GetKey(KeyCode.E))
                {
                    if (gameManager.SKeyValidation())
                    {
                        gameManager.UseSKey();
                        anim.SetTrigger("Open");
                        keyText.SetActive(false);
                        StartCoroutine(MonsterInstantiate());
                    }
                }
            }
            else if(this.gameObject.CompareTag("SilverKeys"))
            {
                if (Input.GetKey(KeyCode.E))
                {
                    itemObject.SetActive(false);
                    interactiveText.SetActive(false);
                    keyText.SetActive(false);
                    gameManager.CollectSKey();
                    //add implementation with inventory soon. For now just hide it.
                    // check condition if chest,

                }
            }
        }
    }
    private IEnumerator GetGoldenKey()
    {
        yield return new WaitForSeconds(7f);
        gameManager.CollectGKey();
        Destroy(this.gameObject);
    }
    private IEnumerator GetScroll()
    {
        yield return new WaitForSeconds(7f);
        Destroy(this.gameObject);
    }
    private IEnumerator MonsterInstantiate()
    {
        yield return new WaitForSeconds(5f);
        monster.SetActive(true);
        Destroy(this.gameObject);
    }
    private void OnTriggerExit(Collider other)
    {
        interactiveText.SetActive(false);
        keyText.SetActive(false);
    }
}
