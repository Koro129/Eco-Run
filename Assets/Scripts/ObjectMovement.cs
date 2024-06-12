using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private bool reset = false;
    [SerializeField] private float resetXPosition = -10f; // Atur nilai x yang menjadi batas untuk mereset
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        // Periksa jika posisi x melebihi nilai resetXPosition
        if (transform.position.x < resetXPosition)
        {
            reset = true;
        }

        if (reset)
        {
            ResetPosition();
        }
    }

    private void ResetPosition()
    {
        transform.position = startPosition;
        reset = false; // Setelah reset, kembalikan nilai reset menjadi false
    }
}
