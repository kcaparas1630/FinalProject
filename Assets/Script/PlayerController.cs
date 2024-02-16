using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 9f;
    public float rotationSpeed = 720f;
    private float horizInput;
    private float vertInput;
    private float ySpeed;
    [SerializeField] private CharacterController player;
    [SerializeField] private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizInput = Input.GetAxis("Horizontal");
        vertInput = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        Vector3 movementDirection = new Vector3(horizInput, 0, vertInput);
        float magnitude = Mathf.Clamp01(movementDirection.magnitude) * speed;
        movementDirection = transform.TransformDirection(movementDirection);
        Vector3 velocity = movementDirection * magnitude;
        player.Move(velocity * Time.deltaTime);
        Vector3 rotation = Vector3.up * Input.GetAxis("Mouse X") * Time.deltaTime * rotationSpeed;
        transform.Rotate(rotation);

        if (movementDirection != Vector3.zero)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }
}
