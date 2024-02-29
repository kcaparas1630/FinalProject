using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    [SerializeField] int maxPage;
    int currentPage;
    Vector3 targetPos;
    private float horizInput;
    private bool isMoving = false;
    [SerializeField] Vector3 pageStep;
    [SerializeField] RectTransform levelPagesRect;

    [SerializeField] float tweenTime;
    [SerializeField] LeanTweenType tweenType;

    private void Awake()
    {
        currentPage = 1;
        targetPos = levelPagesRect.localPosition;
    }
    private void Update()
    {
        horizInput = Input.GetAxisRaw("Horizontal");

        if (horizInput > 0 && !isMoving) 
        {
            Next();
        }
        else if (horizInput < 0 && !isMoving)
        {
            Previous();
        }
    }
    public void Next()
    {
        if (currentPage < maxPage)
        {
            currentPage++;
            targetPos += pageStep;
            MovePage();
        }
    }
    public void Previous()
    {
        if (currentPage > 1)
        {
            currentPage--;
            targetPos -= pageStep;
            MovePage();
        }
    }
    void MovePage()
    {
        isMoving = true;
        levelPagesRect.LeanMoveLocal(targetPos, tweenTime).setEase(tweenType);
    }

}
