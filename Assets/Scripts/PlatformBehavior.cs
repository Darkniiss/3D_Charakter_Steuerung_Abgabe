using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehavior : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    private float timePassed;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (player.playerInteracted)
        {
            timePassed += Time.deltaTime;

            if (timePassed > 5)
            {
                transform.Translate(Vector3.up * Time.deltaTime);
            }
        }
    }
}
