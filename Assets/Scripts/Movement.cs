using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Movement : MonoBehaviour
{
    Rigidbody2D rb;

    

    public float movementSpeed = -1f;
    private float jumpSpeed = 8f;
    public Vector2 direction;
    public Vector2 velocity;
    public LayerMask floorMask;
    public bool isGrounded;
    public GameObject hitObject;
    public Vector2 groundCheckPosition;
    public float groundCheckRadius = 0.05f;
    public SpriteRenderer myRenderer;


    Animator _animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        _animator = GetComponentInChildren<Animator>();

        myRenderer = GetComponent<SpriteRenderer>();

        //blackOut.FadeOut(); //No funcional por ahora, revisar para la pre-alpha
    }

    //Se va a encaragr de leer los inputs y calcular la velocidad
    void Update()
    {
        
            direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

            //direction.Normalize(); //si esta esto desactivado saltará lo mismo en movimiento que parado
            velocity = new Vector2(direction.x * movementSpeed, rb.velocity.y);

            if (isGrounded && Input.GetButton("Jump"))
            {
                velocity = new Vector2(direction.x * movementSpeed, jumpSpeed);
            }

            _animator.SetFloat("speed", Mathf.Abs(velocity[0]));
            _animator.SetFloat("jump", direction.y * movementSpeed);

            if (velocity[0] < -0.01f)
                myRenderer.flipX = true;
            else if (velocity[0] > 0.01f)
                myRenderer.flipX = false;

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

    public void TeleportToStart()
    {
        transform.position = new Vector2(-6f, -3.5f);
    }
}
