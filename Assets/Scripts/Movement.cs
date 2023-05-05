using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    Rigidbody2D rb;

    public float movementSpeed = -1f;
    public float jumpSpeed = 8f;
    public Vector2 direction;
    public Vector2 velocity;
    public LayerMask floorMask;
    public bool isGrounded;
    public GameObject hitObject;
    public Vector2 groundCheckPosition;
    public float groundCheckRadius = 0.05f;
    public float fallMultiplier = 3f;
    public float normalMultiplier = 2f;
    public SpriteRenderer myRenderer;
    private bool checkpointReached = false;

    Animator _animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        _animator = GetComponentInChildren<Animator>();

        myRenderer = GetComponent<SpriteRenderer>();
        //blackOut.FadeOut(); //No funcional por ahora, revisar para la pre-alpha
    }

    //Se va a encargar de leer los inputs y calcular la velocidad
    void Update()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        velocity = new Vector2(direction.x * movementSpeed, rb.velocity.y);

        if (velocity[1] < 0)
        {
            rb.gravityScale = fallMultiplier;
        }

        else
        {
            rb.gravityScale = normalMultiplier;
        }

        if (isGrounded && Input.GetButton("Jump"))
        {

            velocity = new Vector2(direction.x * movementSpeed, jumpSpeed);
        }

        _animator.SetFloat("speedX", Mathf.Abs(velocity.x));
        _animator.SetFloat("speedY", rb.velocity.y);
        _animator.SetBool("isJumping", !isGrounded);

        //_animator.setfloat("jump", direction.y * movementSpeed);

        if (rb.velocity.x < 0)
        {
            myRenderer.flipX = true;
        }
        else if (rb.velocity.x > 0)
        {
            myRenderer.flipX = false;
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

        if (rb.position.y > 50f)
        {
            checkpointReached = true;
        }

    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(groundCheckPosition, groundCheckRadius);
    }

    public void TeleportToStart()
    {
        if (checkpointReached == false)
        {
            transform.position = new Vector2(-6f, -3.5f);
        }
        else
        {
            transform.position = new Vector2(90f, 50f);
        }
    }
}
