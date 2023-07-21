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
    [SerializeField] private new Camera camera;

    private Rigidbody playerRb;
    private Vector2 moveVec;
    private Vector3 dirVec;
    private bool isGrounded;
    private bool isAirborne;
    private bool isInRange;
    public bool playerInteracted;

    [SerializeField] float mouseSens;
    private Vector2 lookVec;
    private float xValue;
    private float yValue;
    //private Vector3 offset = new Vector3(0, -0.5f, 0.25f);


    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    void Update()
    {


        transform.rotation = Quaternion.Euler(0f, camera.transform.rotation.eulerAngles.y, 0f);

        //if(lookVec.x < -10f || lookVec.x > 10f)
        //{
        //    playerRb.velocity = Vector3.one;
        //}

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

        //playerRb.velocity = transform.TransformDirection(dirVec) * moveSpeed * Time.deltaTime;

        //    else if(dirVec.sqrMagnitude == 0f && !isAirborne)
        //{
        //    playerRb.velocity = Vector3.zero;
        //}

    }

    private void LateUpdate()
    {
        //camera.transform.position = transform.position + transform.TransformPoint(offset);
        yValue = Mathf.Clamp(yValue, -200f, 100f);
        camera.transform.rotation = Quaternion.Euler(-yValue * mouseSens, xValue * mouseSens, 0f);

    }

    public void Move(InputAction.CallbackContext context)
    {
        moveVec = context.ReadValue<Vector2>();
        dirVec = new Vector3(moveVec.x, 0f, moveVec.y);

    }

    public void MoveCamera(InputAction.CallbackContext context)
    {
        lookVec = context.ReadValue<Vector2>();
        xValue += lookVec.x;
        yValue += lookVec.y;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (isGrounded && context.started)
        {
            //playerRb.velocity = Vector3.up * jumpForce * Time.deltaTime;
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
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
            //playerRb.velocity = Vector3.zero;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            isAirborne = true;
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
