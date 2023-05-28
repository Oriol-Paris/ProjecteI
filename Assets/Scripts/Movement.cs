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
    public LayerMask hookMask;
    public bool isGrounded;
    public GameObject hitObject;
    public Vector2 groundCheckPosition;
    public float groundCheckRadius = 0.05f;
    public float fallMultiplier = 3f;
    public float normalMultiplier = 2f;
    public SpriteRenderer myRenderer;
    private bool checkpoint1Reached = false;
    private bool checkpoint2Reached = false;
    private bool checkpoint3Reached = false;
    private bool checkpoint4Reached = false;
    public Tutorial_GrapplingGun myGrapplingGun;
    private int extraJumps;
    public int extraJumpsValue;
    private bool jumpRequest;

    public float dashSpeed = 50f;
    public float dashTime = 0.1f;
    public float dashCooldown = 5f;
    private Vector2 dashDirection;
    private bool isDashing = false;
    private float dashTimeLeft;
    private bool isDashOnCooldown = false;

    Animator _animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        myRenderer = GetComponent<SpriteRenderer>();
        extraJumps = extraJumpsValue;
    }

    void Update()
    {
        if (isGrounded == true)
        {
            extraJumps = extraJumpsValue;
        }

        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (velocity[1] < 0)
        {
            rb.gravityScale = fallMultiplier;
        }
        else
        {
            rb.gravityScale = normalMultiplier;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded || extraJumps > 0)
            {
                jumpRequest = true;
                extraJumps--;
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && !isDashOnCooldown && !myGrapplingGun.grappleRope.isGrappling && isGrounded)
        {
            isDashing = true;
            dashTimeLeft = dashTime;
            dashDirection = direction;
            StartCoroutine(DashCooldown());
        }

        _animator.SetFloat("speedX", Mathf.Abs(velocity.x));
        _animator.SetFloat("speedY", rb.velocity.y);
        _animator.SetBool("isJumping", !isGrounded);

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

    void FixedUpdate()
    {
        Collider2D collider = Physics2D.OverlapCircle(groundCheckPosition, groundCheckRadius, floorMask);

        if (collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (isDashing)
        {
            if (dashTimeLeft <= 0)
            {
                isDashing = false;
                rb.velocity = Vector2.zero;
            }
            else
            {
                rb.velocity = new Vector2(dashDirection.x * dashSpeed, dashDirection.y * dashSpeed);
                dashTimeLeft -= Time.fixedDeltaTime;
            }
        }
        else
        {
            velocity = new Vector2(direction.x * movementSpeed, rb.velocity.y);

            if (jumpRequest)
            {
                velocity = new Vector2(direction.x * movementSpeed, jumpSpeed);
                jumpRequest = false;
            }

            if (!myGrapplingGun.grappleRope.isGrappling)
            {
                rb.velocity = velocity;
            }
        }

        if (rb.position.x < 20 && rb.position.y > 9)
        {
            checkpoint1Reached = true;
        }

        if (rb.position.x > 70 && rb.position.y > 15)
        {
            checkpoint2Reached = true;
        }
        if (rb.position.y > 50f)
        {
            checkpoint3Reached = true;
        }
        if (rb.position.y > 85f)
        {
            checkpoint4Reached = true;
        }
    }

    IEnumerator DashCooldown()
    {
        isDashOnCooldown = true;
        yield return new WaitForSeconds(dashCooldown);
        isDashOnCooldown = false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(groundCheckPosition, groundCheckRadius);
    }

    public void TeleportToStart()
    {
        if (checkpoint1Reached == false)
        {
            transform.position = new Vector2(-6f, -3.5f);
        }

        else if (checkpoint1Reached == true && checkpoint2Reached == false)
        {
            transform.position = new Vector2(19f, 9.5f);
        }

        else if (checkpoint2Reached == true && checkpoint3Reached == false)
        {
            transform.position = new Vector2(70f, 17.5f);
        }

        else if (checkpoint3Reached == true && checkpoint4Reached == false)
        {
            transform.position = new Vector2(90f, 50f);
        }

        else if (checkpoint4Reached == true)
        {
            transform.position = new Vector2(104.5f, 86.5f);
        }
    }
}
