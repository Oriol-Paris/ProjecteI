using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
   
    public Transform destination;

    public bool isTeleport;
   
    // Start is called before the first frame update
    void Start()
    {
        if (isTeleport == true)
        {
            destination = GameObject.FindGameObjectWithTag("Portal Destination").GetComponent<Transform>();
        }
    }

    

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"{other.name} ha entrado");
        other.transform.position = new Vector2(destination.position.x, destination.position.y);
    }
}
