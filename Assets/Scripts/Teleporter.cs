using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    Transform player;
    [SerializeField] Transform destination;

   
    public AudioSource audioSource;
    public AudioClip teleportSound;
    

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (destination == null)
            destination = transform.GetChild(0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player.gameObject)
        {
            player.position = destination.position;

            audioSource.PlayOneShot(teleportSound);
        }
    }
}



