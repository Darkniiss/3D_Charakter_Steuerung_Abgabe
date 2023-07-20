using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Camera camera;

    private Rigidbody playerRb;
    private Vector2 moveVec;
    private Vector3 dirVec;

    [SerializeField] float mouseSens;
    private Vector2 lookVec;
    private float xValue;
    private float yValue;
    private Vector3 offset = new Vector3(0, -0.5f, 0.25f);


    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        

            transform.rotation = Quaternion.Euler(0f, camera.transform.rotation.eulerAngles.y, 0f);

            playerRb.velocity = transform.TransformDirection(dirVec) * moveSpeed * Time.deltaTime;
        
    }

    private void LateUpdate()
    {
        camera.transform.position = transform.position + transform.TransformPoint(offset);
        yValue = Mathf.Clamp(yValue, -200f, 100f);
        camera.transform.rotation = Quaternion.Euler(-yValue * mouseSens, xValue * mouseSens, 0f);

    }

    public void Move(InputAction.CallbackContext context)
    {
        moveVec = context.ReadValue<Vector2>();
        dirVec = new Vector3 (moveVec.x, 0f, moveVec.y);
       
    }

    public void MoveCamera(InputAction.CallbackContext context)
    {
        lookVec = context.ReadValue<Vector2>();
        xValue += lookVec.x;
        yValue += lookVec.y;
    }

}
