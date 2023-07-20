using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float smoothTime;

    private Rigidbody playerRb;
    private float curVelocity;
    private Vector2 moveVec;
    private Vector3 dirVec;
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (moveVec.sqrMagnitude != 0)
        {

            float yRotation = Mathf.Atan2(dirVec.x, dirVec.z) * Mathf.Rad2Deg;
            float smoothYRot = Mathf.SmoothDampAngle(transform.eulerAngles.y, yRotation, ref curVelocity, smoothTime);
            transform.rotation = Quaternion.Euler(0f, smoothYRot, 0f);

            playerRb.velocity = dirVec * moveSpeed * Time.deltaTime;
        }
        else
        {
            playerRb.velocity = Vector3.zero;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveVec = context.ReadValue<Vector2>();
        dirVec = new Vector3(moveVec.x, 0f, moveVec.y);
    }
}
