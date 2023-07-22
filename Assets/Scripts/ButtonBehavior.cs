using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{
    private PlayerController player;
    private bool wasPressed;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (player.playerInteracted && !wasPressed)
        {
            transform.position -= new Vector3(0f, 0.05f, 0f);
            wasPressed = true;
        }
    }
}
