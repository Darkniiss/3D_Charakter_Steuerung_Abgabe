using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float mouseSens;
    [SerializeField] private float gamepadSens;
    [SerializeField] private new Camera camera;

    private float sensitivity;
    private bool isInRange;
    private bool usingMouse;
    private bool usingGamepad;
    private Rigidbody playerRb;
    private Vector2 moveVec;
    private Vector3 dirVec;

    public float xValueMouse;
    public float yValueMouse;
    public float xValueGamepad;
    public float yValueGamepad;
    public bool isGrounded;
    public bool isAirborne;
    public bool playerInteracted;
    public Vector2 lookVec;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    void Update()
    {


        transform.rotation = Quaternion.Euler(0f, camera.transform.rotation.eulerAngles.y, 0f);

        if (dirVec.magnitude != 0f)
        {

            if (isAirborne)
            {
                playerRb.AddForce(transform.TransformDirection(dirVec) * moveSpeed * Time.deltaTime / 2, ForceMode.VelocityChange);

            }
            else
            {
                playerRb.AddForce(transform.TransformDirection(dirVec) * moveSpeed * Time.deltaTime, ForceMode.VelocityChange);
            }
        }
        else if (dirVec.magnitude == 0f && isGrounded)
        {
            playerRb.velocity = Vector3.zero;
        }
    }

    private void LateUpdate()
    {
        if (usingMouse)
        {

        yValueMouse = Mathf.Clamp(yValueMouse, -200f, 100f);
        camera.transform.rotation = Quaternion.Euler(-yValueMouse * sensitivity, xValueMouse * sensitivity, 0f);
        }
        else
        {
            xValueGamepad += lookVec.x;
            yValueGamepad += lookVec.y;
            yValueGamepad = Mathf.Clamp(yValueGamepad, -200f, 100f);
            camera.transform.rotation = Quaternion.Euler(-yValueGamepad * sensitivity, xValueGamepad * sensitivity, 0f);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveVec = context.ReadValue<Vector2>();
        dirVec = new Vector3(moveVec.x, 0f, moveVec.y);
    }

    public void MoveCameraMouse(InputAction.CallbackContext context)
    {
        sensitivity = mouseSens;
        lookVec = context.ReadValue<Vector2>();
        xValueMouse += lookVec.x;
        yValueMouse += lookVec.y;
        usingMouse = true;
    }

    public void MoveCameraGamepad(InputAction.CallbackContext context)
    {
        sensitivity = gamepadSens;
        lookVec = context.ReadValue<Vector2>();
        usingMouse = false;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (isGrounded && context.started)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            isAirborne = true;
            isGrounded = false;
        }

    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (isInRange)
        {
            playerInteracted = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isAirborne = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        isInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isInRange = false;
    }
}
