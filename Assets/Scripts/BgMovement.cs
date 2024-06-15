using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float resetXFrom = -10f;
    [SerializeField] private float resetXTo = -10f;

    private void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        if(transform.position.x <= resetXFrom)
        {
            ResetPosition();
        }
    }

    private void ResetPosition()
    {
        transform.position = new Vector3(resetXTo, transform.position.y, transform.position.z);
    }
}