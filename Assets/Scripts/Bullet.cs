using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float range;
    private int damage;
    private float speed;
    private bool shootRight;
    private Vector2 direction;
    private float startTime;

    // Initialize the bullet with the specified parameters
    public void Initialize(float range, int damage, float speed, bool shootRight)
    {
        this.range = range;
        this.damage = damage;
        this.speed = speed;
        this.shootRight = shootRight;
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

    // Move the bullet based on its direction and speed
    private void MoveBullet()
    {
        transform.Translate(direction * Time.deltaTime);
    }

    // Check if the bullet has reached its maximum range
    private void CheckRange()
    {
        float distance = (Time.time - startTime) * speed;
        if (distance >= range)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (shootRight && collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (!shootRight && collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
