using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables
    [SerializeField] private Animator animator;
    [SerializeField] private float laneDistance = 2.0f; // Distance between lanes
    [SerializeField] private float jumpForce = 5.0f; // Jump force
    [SerializeField] private float gravity = 10.0f; // Gravity
    [SerializeField] private float glideGravity = 1.0f; // Glide duration
    [SerializeField] private float slideDuration = 1.0f; // Slide duration
    [SerializeField] private float groundDistance = 1.0f; // Ground distance
    [SerializeField] private GameObject groundParticle;
    [SerializeField] private LayerMask groundLayer; // Ground layer
    [SerializeField] private Transform feetpos;
    [SerializeField] private Gun gun;

    // public ParticleSystem dust;

    public int currentLane { get; private set; }
    public bool isSliding { get; private set; } = false;

    private bool isJumping = false;
    private bool isGrounded;
    private float slideTimer = 0f;

    private Rigidbody2D rb;
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        CurrentLane();
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

    private void CurrentLane()
    {
        if (transform.position.y < -30)
        {
            currentLane = 2;
        }
        else if (transform.position.y < -27)
        {
            currentLane = 1;
        }
        else
        {
            currentLane = 0;
        }
        Debug.Log("Current Lane set to: " + currentLane);
    }

    private bool isChangingLane = false;
    
    private void ChangeLane(int direction)
    {
        if (isChangingLane) return;
    
        currentLane += direction;
        Vector3 targetPosition = transform.position;
        targetPosition.y = transform.position.y - (direction * laneDistance);
        StartCoroutine(MoveToPosition(targetPosition, 0.35f));
    }
    
    private IEnumerator MoveToPosition(Vector3 targetPosition, float duration)
    {
        isChangingLane = true;
        float elapsedTime = 0;
        Vector3 startingPosition = transform.position;
    
        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startingPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    
        transform.position = targetPosition;
        isChangingLane = false;
    }

    private void HandleFalling()
    {
        if (!IsGrounded() && !isChangingLane) // Add check for isChangingLane
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
        animator.SetBool("isJumping", true);
    }

    private void StartSlide()
    {
        isSliding = true;
        slideTimer = slideDuration;
        animator.SetBool("isSliding", true);
    }

    private void HandleSliding()
    {
        if (isSliding)
        {
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                isSliding = false;
                animator.SetBool("isSliding", false);
            }
            slideTimer -= Time.deltaTime;

            if (slideTimer <= 0)
            {
                isSliding = false;
                animator.SetBool("isSliding", false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var otherScript = other.gameObject.GetComponent<ObjectMovement>();

        if (otherScript != null)
        {
            var otherLane = otherScript.currentLane;

            if (other.CompareTag("Coin") && currentLane == otherLane)
            {
                other.gameObject.GetComponent<Health>().TakeDamage(1);
            }
            
            if (isSliding && other.CompareTag("Destroyable") && currentLane == otherLane)
            {
                other.gameObject.GetComponent<Health>().TakeDamage(1);
            }
        }
    }
    
    private void Shoot()
    {
        if (gun != null)
        {
            gun.Shoot(currentLane);
        }
    }

    private bool IsGrounded()
    {
        string groundTag = "Ground " + currentLane;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(feetpos.position, groundDistance, groundLayer);

        isGrounded = false;

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag(groundTag))
            {
                isGrounded = true;
                break;
            }
        }

        if (isGrounded)
        {
            if (rb.velocity.y < 0)
            {
                isJumping = false;
                animator.SetBool("isJumping", false);
                Instantiate(groundParticle, feetpos.position, Quaternion.Euler(-90, 0, 0));
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
        }

        return isGrounded;
    }
}
