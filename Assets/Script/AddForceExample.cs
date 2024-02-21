using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceExample : MonoBehaviour
{
    public float forceAmount = 1000f;
    [SerializeField] Rigidbody rb;
    private void OnMouseDown()
    {
        Debug.Log("I clicked the door!");
        Debug.Log(-transform.forward);
        rb.AddForce(-transform.forward * forceAmount, ForceMode.Acceleration);
        rb.useGravity = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
