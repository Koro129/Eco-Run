using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPlayerControl : MonoBehaviour
{
    public float movSpeed;
    float speedX, speedY;
    private Vector3 originalScale;
    private Animator anim;
    Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        originalScale = transform.localScale;
    }

    void Update()
    {
        speedX = Input.GetAxisRaw("Horizontal") * movSpeed;
        speedY = Input.GetAxisRaw("Vertical") * movSpeed;
        rb.velocity = new Vector2(speedX, speedY);
        if (speedX < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
        else if (speedX > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
        
        anim.SetBool("isRun", speedX != 0);
    }
}
