using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 9f;
    public float rotationSpeed = 100f;
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
    [SerializeField] private AudioSource movementAudioSource;
    [SerializeField] private Camera cam;                // a reference to the main camera
    [SerializeField] private CinemachineFreeLook freeLook; // reference to freeLook Camera
    [SerializeField] private CinemachineVirtualCamera virtualCamera; // a reference to virtual camera.
    private float rotateToFaceAwayFromCameraSpeed = 5f; // the speed to rotate our Player to align with the camera view.
    private bool isTorchGrabbingAnimationPlaying = false;
    private bool isDoorOpeningAnimationPlaying = false;
    private bool isCutscenePlaying = false;

    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    private void Awake()
    {
        Messenger.AddListener(GameEvent.CUTSCENE_PLAYING, OnCutscenePlaying);
        Messenger.AddListener(GameEvent.CUTSCENE_PLAYING, OnCutsceneFinished);
    }
    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.CUTSCENE_PLAYING, OnCutscenePlaying);
        Messenger.RemoveListener(GameEvent.CUTSCENE_PLAYING, OnCutsceneFinished);
    }
    private void OnCutscenePlaying()
    {
        isCutscenePlaying = true;
        StopMovement();
    }
    private void OnCutsceneFinished()
    {
        isCutscenePlaying = false;
    }
    private void StopMovement()
    {
        isCutscenePlaying = true;
        // Stop movement when the cutscene is playing
        horizInput = 0f;
        vertInput = 0f;
        animator.SetFloat("Velocity", 0f);
        movementAudioSource.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCutscenePlaying)
        {
            horizInput = Input.GetAxis("Horizontal");
            vertInput = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(horizInput, 0, vertInput).normalized;
            animator.SetFloat("Velocity", movement.magnitude);

            if (torch != null && torch.activeSelf)
            {
                animator.SetBool("MovingWithTorch", true);
                animator.SetFloat("Velocity", movement.magnitude);
            }
            // convert from local to global coordinates
            movement = transform.TransformDirection(movement);
            // Rotate the player to face the movement direction
            if (movement.magnitude > 0)
            {
                RotateToFaceMovement(movement);
                RotatePlayerToFaceAwayFromCamera();
            }

            // Calculate movement direction including gravity
            Vector3 moveDirection = movement * speed + Vector3.up * yVelocity;

            // Apply gravity
            yVelocity += gravity * Time.deltaTime;

            // Move the player using the CharacterController
            player.Move(moveDirection * Time.deltaTime);

            // Play movement audio if moving
            if (movement.magnitude > 0 && !movementAudioSource.isPlaying)
            {
                movementAudioSource.Play();
            }
            // Stop movement audio if not moving
            else if (movement.magnitude == 0 && movementAudioSource.isPlaying)
            {
                movementAudioSource.Stop();
            }
        }
        else
        {
            // Stop movement when a cutscene is playing
            horizInput = 0f;
            vertInput = 0f;
            animator.SetFloat("Velocity", 0f);
            movementAudioSource.Stop();
        }
    }


    public void SetTorchGrabbingAnimationPlaying(bool value)
    {
        isTorchGrabbingAnimationPlaying = value;
    }

    public void SetDoorOpeningAnimationPlaying(bool value)
    {
        isDoorOpeningAnimationPlaying = value;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Staircase") || other.gameObject.CompareTag("Hallway"))
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

    private IEnumerator IdleChangeAnimOnDelay()
    {
        yield return new WaitForSeconds(5f);

        animator.SetTrigger("IdleWait");
    }

}
