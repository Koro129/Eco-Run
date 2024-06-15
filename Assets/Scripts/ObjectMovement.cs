using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    public int currentLane { get; private set; }

    private void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    private int GetCurrentLane(string tag)
    {
        switch (tag)
        {
            case "Ground 0":
                return 0;
            case "Ground 1":
                return 1;
            case "Ground 2":
                return 2;
            default:
                return 1; // Default to middle lane if tag is not recognized
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground 0") || other.CompareTag("Ground 1") || other.CompareTag("Ground 2"))
        {
            currentLane = GetCurrentLane(other.tag);
        }
    }
}
