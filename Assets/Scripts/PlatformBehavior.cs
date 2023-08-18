using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehavior : MonoBehaviour
{

    [SerializeField] private float upSpeed;

    private float timePassed;
    private PlayerController player;
    private Rigidbody platformRb;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        platformRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (player.playerInteracted)
        {
            timePassed += Time.deltaTime;

            if (timePassed > 5)
            {
                platformRb.velocity = Vector3.up * upSpeed * Time.deltaTime;
            }
        }
    }
}
