using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float range;
    private int damage;
    private float speed;
    private bool shootRight;
    private Vector2 direction;
    private float startTime;
    private int currentLane;


    // Initialize the bullet with the specified parameters
    public void Initialize(float range, int damage, float speed, bool shootRight, int currentLane)
    {
        this.range = range;
        this.damage = damage;
        this.speed = speed;
        this.shootRight = shootRight;
        this.currentLane = currentLane;
        startTime = Time.time;
    }

    // Set the direction of the bullet
    public void SetDirection(Vector2 direction)
    {
        this.direction = direction.normalized * speed;
    }

    private void Update()
    {
        MoveBullet();
        CheckRange();
    }

    private void MoveBullet()
    {
        transform.Translate(direction * Time.deltaTime);
    }

    private void CheckRange()
    {
        float distance = (Time.time - startTime) * speed;
        if (distance >= range)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (shootRight)
        {
            if (other.CompareTag("Enemy") && currentLane == other.gameObject.GetComponent<ObjectMovement>().currentLane)
            {
                Health healthComponent = other.gameObject.GetComponent<Health>();
                if (healthComponent != null)
                {
                    healthComponent.TakeDamage(damage);
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            if (other.CompareTag("Player") && currentLane == other.gameObject.GetComponent<PlayerController>().currentLane)
            {
                Health healthComponent = other.gameObject.GetComponent<Health>();
                if (healthComponent != null)
                {
                    healthComponent.TakeDamage(damage);
                    Destroy(gameObject);
                }
            }
        }
    }
}
