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
    private float jumpSpeed = 2f;
    public Vector2 direction;
    public Vector2 velocity;
    public LayerMask floorMask;
    public bool isGrounded;
    public bool hasJumped = false;
    private int jumpCount = 2;
    public int currentJump = 0;
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
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;


        velocity = new Vector2(direction.x * movementSpeed, rb.velocity.y);

        //if (isGrounded && Input.GetButtonDown("Vertical"))
        //{
        //    velocity = new Vector2(direction.x * movementSpeed, jumpSpeed);
        
       if (isGrounded && Input.GetAxisRaw("Vertical") > 0f && !hasJumped) 
        {
            //velocity = new Vector2(direction.x * movementSpeed, direction.y * movementSpeed);
            rb.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
            hasJumped = true;
            currentJump++;
            
        }

        if (!isGrounded && currentJump < jumpCount && Input.GetKeyDown(KeyCode.W) && hasJumped)
        {
            //velocity = new Vector2(direction.x * movementSpeed, direction.y * movementSpeed);
            //rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(new Vector2(0, jumpSpeed * 1.5f), ForceMode2D.Impulse);
            //rb.AddForce(new Vector2(direction.x * movementSpeed, direction.y * movementSpeed), ForceMode2D.Impulse);
            hasJumped = true;
            currentJump++;
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
            hasJumped = false;
            currentJump = 0;
        }
        else
        {
            isGrounded = false;
        }

        //if (Input.GetAxis("Horizontal") > 0.1f || Input.GetAxis("Horizontal") < -0.1f || Input.GetAxis("Vertical") > 0.1f)
            rb.velocity = velocity;


    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(groundCheckPosition, groundCheckRadius);
    }

    public void TeleportToStart()
    {
        transform.position = new Vector2(-6f, -3.5f);
    }

}
