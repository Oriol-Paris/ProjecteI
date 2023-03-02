using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float unitsMovementSpeed = 0.001f;
    Rigidbody2D rg2d = null;

    private Vector2 velocity;
    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        rg2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
      

    }

    private void FixedUpdate()
    {
       
    }
}
