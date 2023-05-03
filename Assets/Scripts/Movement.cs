using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    Rigidbody2D rb;

    public float movementSpeed = -1f;
<<<<<<< Updated upstream
    public float jumpSpeed = -1f;
=======
    public float jumpSpeed = 8f;
>>>>>>> Stashed changes
    public Vector2 direction;
    public Vector2 velocity;
    public LayerMask floorMask;
    public bool isGrounded;
    public GameObject hitObject;
    public Vector2 groundCheckPosition;
    public float groundCheckRadius = 0.05f;
<<<<<<< Updated upstream
    public BlackOut blackOut;
=======
    public float fallMultiplier = 3f;
    public float normalMultiplier = 2f;
    public SpriteRenderer myRenderer;

>>>>>>> Stashed changes

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
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

<<<<<<< Updated upstream
        if (!Input.GetButton("Jump"))
        {
            direction.Normalize();
        }

        velocity = new Vector2(direction.x * movementSpeed, rb.velocity.y);

        if (isGrounded && Input.GetButtonDown("Jump"))
=======
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
>>>>>>> Stashed changes
        {

            velocity = new Vector2(direction.x * movementSpeed, jumpSpeed);
        }

        _animator.SetFloat("speed", Mathf.Abs(velocity.x));
        
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
