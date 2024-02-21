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
    private float gravity = -9.81f; // downward pull of gravity
    [SerializeField] private CharacterController player;
    [SerializeField] private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;

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
        ySpeed += gravity * Time.deltaTime;
        Vector3 velocity = movementDirection * magnitude;
        velocity.y = ySpeed;
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

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        float pushForce = 5.0f;
        // get the rigidbody of the thing we collided with (if it has one)
        Rigidbody body = hit.collider.attachedRigidbody;
        // if a rigidbody was found (and it's not kinematic), apply a force
        // to it in the same direction that it was hit
        if (body != null && !body.isKinematic)
        {
            body.velocity = hit.moveDirection * pushForce;
        }
    }
}
