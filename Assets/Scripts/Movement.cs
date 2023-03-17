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

    Animator _animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        _animator = GetComponentInChildren<Animator>();
    }

    //Se va a encaragr de leer los inputs y calcular la velocidad
    void Update()
    {
        //axis.Y es 0 porque no se modifica la velocidad vertical a excepcion del salto.
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        /*Tambien se puede hacer asi.
         * axis.x = Input.GetAxisRaw("Horizontal");
         * axis.y = Input.GetAxisRaw("Vertical");
         */

        //Haze que la velocidad este en un rango de -1 a 1
        direction.Normalize();
        velocity = new Vector2(direction.x * movementSpeed, rb.velocity.y);

        _animator.SetFloat("speed", velocity[0]);
        _animator.SetFloat("jump", direction.y * movementSpeed);

        if (isGrounded && Input.GetAxisRaw("Vertical") > 0) 
        {
            velocity = new Vector2(direction.x * movementSpeed, direction.y * movementSpeed);
        }
    }

    //Se va a encargar de mover el player
    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 0.5f, direction, 1f, floorMask);

        if (hit)
        {
            hitObject = hit.collider.gameObject;
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
        Vector2 groundCheckPosition = new Vector2(transform.position.x, transform.position.y) + (Vector2.up * -1);
        float groundCheckRadius = 0.5f;
        Gizmos.DrawWireSphere(groundCheckPosition, groundCheckRadius);
    }


}
