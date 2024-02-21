using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRigidBody : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    float horiz;
    float vert;
    float speed = 10f;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        horiz = Input.GetAxis("Horizontal");
        vert = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(horiz, 0, vert) * speed * Time.deltaTime * 100;
        rb.velocity = movement;
    }

}
