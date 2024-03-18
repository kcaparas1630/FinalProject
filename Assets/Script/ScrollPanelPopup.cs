using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollPanelPopup : MonoBehaviour
{
   public void Open()
    {
        this.gameObject.SetActive(true);
    }
    public void Close()
    {
        this.gameObject.SetActive(false);
    }
}
