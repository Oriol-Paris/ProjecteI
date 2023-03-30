using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/*
Vector2 groundCheckPosition = transform.position + (Vector2.up * -1);
float groundCheckRadius = 0.5f;
Gizmos.DrawWireSphere(groundCheckPosition, groundCheckRadius);
*/

public class Movement : MonoBehaviour
{
    Rigidbody2D rb;

    public float movementSpeed = -1f;
    public Vector2 direction;
    public Vector2 velocity;
    public LayerMask floorMask;
    public bool isGrounded;
    public GameObject hitObject;
    public Vector2 groundCheckPosition;
    public float groundCheckRadius = 0.05f;

    Animator _animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        _animator = GetComponentInChildren<Animator>();
    }

    //Se va a encaragr de leer los inputs y calcular la velocidad
    void Update()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        direction.Normalize();
        velocity = new Vector2(direction.x * movementSpeed, rb.velocity.y);

        _animator.SetFloat("speed", velocity[0]);
        _animator.SetFloat("jump", direction.y * movementSpeed);

        if (isGrounded && Input.GetAxisRaw("Vertical") > 0) 
        {
            velocity = new Vector2(direction.x * movementSpeed, direction.y * movementSpeed);
        }

        groundCheckPosition = new Vector2(transform.position.x, transform.position.y - 0.6f);
    }

    //Se va a encargar de mover el player
    void FixedUpdate()
    {
        Collider2D collider = Physics2D.OverlapCircle(groundCheckPosition, groundCheckRadius, floorMask);

        if (collider)
        {
            hitObject = collider.gameObject;
        }
        else
        {
            hitObject = null;
        }
        if (hitObject != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        rb.velocity = velocity;


    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(groundCheckPosition, groundCheckRadius);
    }


}
