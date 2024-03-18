using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UpdateUIManager : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI Skeys;
    [SerializeField] TextMeshProUGUI Gkeys;

   public int GetKeyCount()
    {
        return int.Parse(Skeys.text);
    }
    public void UpdateSKeyCount(int count)
    {
        Skeys.text = count.ToString();
    }
    public void UpdateGKeyCount(int count)
    {
        Gkeys.text = count.ToString();
    }

}
