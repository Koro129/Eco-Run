using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float laneDistance = 2.0f; // Distance between lanes
    [SerializeField] private float jumpForce = 5.0f; // Jump force
    [SerializeField] private float gravity = 10.0f; // Gravity
    [SerializeField] private float glideGravity = 1.0f; // Glide duration
    [SerializeField] private float slideDuration = 1.0f; // Slide duration
    [SerializeField] private float groundDistance = 1.0f; // Ground distance
    [SerializeField] private LayerMask groundLayer; // Ground layer
    [SerializeField] private Transform feetpos;
    [SerializeField] private Gun gun;

    private int currentLane = 1; // Current lane: 0 = top, 1 = middle, 2 = bottom
    private bool isJumping = false;
    private bool isGrounded;
    // private bool isSliding = false;
    public bool isSliding { get; private set; } = false;
    private float slideTimer = 0f;

    private Rigidbody2D rb;
    private Vector3 startPosition; // Initial position of the player

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position; // Store the initial position
    }

    private void Update()
    {
        // HandleInput();
        HandleSliding();
        HandleFalling();
    }

    public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W) && currentLane > 0 && !isJumping && !isSliding)
        {
            ChangeLane(-1);
        }
        if (Input.GetKeyDown(KeyCode.S) && currentLane < 2 && !isJumping && !isSliding)
        {
            ChangeLane(1);
        }
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping && !isSliding && IsGrounded())
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isJumping && !isSliding && IsGrounded())
        {
            StartSlide();
        }
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void ChangeLane(int direction)
    {
        currentLane += direction;
        Vector3 targetPosition = transform.position;
        targetPosition.y = transform.position.y - (direction * laneDistance);
        transform.position = targetPosition;
    }

    private void HandleFalling()
    {
        if (!IsGrounded())
        {
            if (Input.GetKey(KeyCode.Space) && isJumping)
            {

                rb.velocity += Vector2.down * glideGravity * gravity * Time.deltaTime;
            }
            else
            {
                rb.velocity += Vector2.down * gravity * Time.deltaTime;
            }
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isJumping = true;
    }

    private void StartSlide()
    {
        isSliding = true;
        slideTimer = slideDuration;
        // animator.SetTrigger("Slide");
    }

    private void HandleSliding()
    {
        if (isSliding)
        {
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                isSliding = false;
            }
            // Debug.Log("Sliding");
            slideTimer -= Time.deltaTime;

            if (slideTimer <= 0)
            {
                isSliding = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            Debug.Log("Coin collected");
            other.gameObject.GetComponent<Health>().TakeDamage(1);
        }
        
        if (isSliding && other.CompareTag("Destroyable"))
        {
            Debug.Log("Collision with " + other.gameObject.name);
            other.gameObject.GetComponent<Health>().TakeDamage(1);
            // Destroy(other.gameObject);
        }
    }

    private void Shoot()
    {
        if (gun != null)
        {
            gun.Shoot();
        }
    }
    
    private bool IsGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(feetpos.position, groundDistance, groundLayer);
        if (isGrounded)
        {
            if (rb.velocity.y < 0)
            {
                isJumping = false;
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
        }
        return isGrounded;
    }
}
