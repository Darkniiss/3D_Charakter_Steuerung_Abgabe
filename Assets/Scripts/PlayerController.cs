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
    public Vector2 moveVec;
    public Vector3 dirVec;

    [SerializeField] float mouseSens;
    public Vector2 lookVec;
    private float xValue;
    private float yValue;

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
        camera.transform.position = transform.position;
        if (lookVec.sqrMagnitude == 0) return;
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
