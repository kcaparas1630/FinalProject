using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkthroughController : MonoBehaviour
{
    [SerializeField] private GameObject movementKeyText;
    [SerializeField] private GameObject interactionKeyText;
    [SerializeField] private GameObject torchWaveKeyText;
    [SerializeField] private List<GameObject> walkthroughCircles = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(textShow());
    }
    private void Awake()
    {
        Messenger.AddListener(GameEvent.OPEN_CANVAS, OnOpenCanvas);
        Messenger.AddListener(GameEvent.WALKTHROUGH_CIRCLE, OnWalkthroughCircle);
    }
    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.OPEN_CANVAS, OnOpenCanvas);
        Messenger.RemoveListener(GameEvent.WALKTHROUGH_CIRCLE, OnWalkthroughCircle);
    }

    private void OnWalkthroughCircle()
    {
        movementKeyText.SetActive(false);
        interactionKeyText.SetActive(true);
    }
    private void OnOpenCanvas()
    {
        interactionKeyText.SetActive(false);
        // Activate all walkthrough circles
        foreach (GameObject circle in walkthroughCircles)
        {
            circle.SetActive(true);
        }
    }
  
    IEnumerator textShow()
    {
        yield return new WaitForSeconds(3.5f);
        movementKeyText.SetActive(true);
    }
  
}
