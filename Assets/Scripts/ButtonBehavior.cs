using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{
    private bool wasPressed;
    private PlayerController player;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();

    }
    void Update()
    {
        if (player.playerInteracted && !wasPressed)
        {
            transform.position -= new Vector3(0f, 0.05f, 0f);
            wasPressed = true;
        }
    }
}
