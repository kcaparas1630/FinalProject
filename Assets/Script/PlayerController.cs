using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 9f;
    public float rotationSpeed = 720f;
    private float horizInput;
    private float vertInput;
    private float ySpeed;
    private float gravity = -9.81f; // downward pull of gravity
    private float yVelocity = 0f;       // current y Velocity
    private float rotateToFaceMovementSpeed = 5f;
    [SerializeField] private CharacterController player;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject torch;
    [SerializeField] private GameObject model;
    [SerializeField] private Camera cam;                // a reference to the main camera
    [SerializeField] private CinemachineFreeLook freeLook; // reference to freeLook Camera
    [SerializeField] private CinemachineVirtualCamera virtualCamera; // a reference to virtual camera.
    private float rotateToFaceAwayFromCameraSpeed = 5f; // the speed to rotate our Player to align with the camera view.
   

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
        Vector3 movement = new Vector3(horizInput, 0, vertInput);
        // ensure diagonal movement doesn't exceed horiz/vert movement speed
        movement = Vector3.ClampMagnitude(movement, 1.0f);
        animator.SetFloat("Velocity", movement.magnitude);

        if (torch != null && torch.activeSelf)
        {
            animator.SetBool("MovingWithTorch", true);
            animator.SetFloat("Velocity", movement.magnitude);
        }
        // convert from local to global coordinates
        movement = transform.TransformDirection(movement);
        if (movement.magnitude > 0)
        {
            RotateToFaceMovement(movement);
            RotatePlayerToFaceAwayFromCamera();
        }
        movement *= speed;

        // calculate yVelocity and add it to the player's movement vector
        yVelocity += gravity * Time.deltaTime;

        movement.y = yVelocity;
        movement *= Time.deltaTime; // make all movement processor independent
        // move the player  (using the character controller)
        player.Move(movement);
     
    }

    void RotateToFaceMovement(Vector3 moveDirection)
    {
        Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
        model.transform.rotation = Quaternion.Slerp(model.transform.rotation, newRotation, rotateToFaceMovementSpeed * Time.deltaTime);
    }
    private void RotatePlayerToFaceAwayFromCamera()
    {
        // isolate the camera's Y rotation
        Quaternion camRotation = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y, 0);

        // set the player's rotation
        //transform.rotation = camRotation;

        // replace the above line with this one to enable smoothing
        transform.rotation = Quaternion.Slerp(transform.rotation, camRotation, rotateToFaceAwayFromCameraSpeed * Time.deltaTime);
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

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Staircase"))
        {
            freeLook.enabled = false;
            virtualCamera.enabled = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Staircase"))
        {
            freeLook.enabled = true;
            virtualCamera.enabled = false;
        }
    }

    private IEnumerator IdleChangeAnimOnDelay()
    {
        yield return new WaitForSeconds(5f);

        animator.SetTrigger("IdleWait");
    }

}
