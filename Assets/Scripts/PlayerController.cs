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
    [SerializeField] private float slideSpeed = 5.0f; // Slide speed
    [SerializeField] private float groundDistance = 1.0f; // Ground distance
    [SerializeField] private LayerMask groundLayer; // Ground layer
    [SerializeField] private Transform feetpos;

    private int currentLane = 1; // Current lane: 0 = top, 1 = middle, 2 = bottom
    private bool isJumping = false;
    private bool isGliding = false;
    private bool isGrounded;
    private bool isSliding = false;
    private float slideTimer = 0f;
    private bool spacePressed = false;

    private Rigidbody2D rb;
    private Vector3 startPosition; // Initial position of the player

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position; // Store the initial position
    }

    private void Update()
    {
        HandleInput();
        HandleSliding();
        HandleFalling();
    }

    private void HandleInput()
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
            slideTimer -= Time.deltaTime;

            if (slideTimer <= 0)
            {
                isSliding = false;
            }
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
