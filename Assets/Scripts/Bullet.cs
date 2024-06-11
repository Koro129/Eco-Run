using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float range;
    private int damage;
    private float speed; // tambahan parameter speed
    private Vector2 direction;
    private float startTime;

    public void Initialize(float range, int damage, float speed) // menambah parameter speed
    {
        this.range = range;
        this.damage = damage;
        this.speed = speed; // menyimpan speed yang diterima
        startTime = Time.time;
    }

    public void SetDirection(Vector2 direction)
    {
        this.direction = direction.normalized * speed; // mengatur kecepatan berdasarkan arah
    }

    private void Update()
    {
        transform.Translate(direction * Time.deltaTime); // menggunakan Translate untuk pergerakan
        float distance = (Time.time - startTime) * speed;
        if (distance >= range)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log("Hit " + other.gameObject.name);
        if (collision.CompareTag("Enemy"))
        {
            // Debug.Log("Enemy Hit");
            // Destroy(collision.gameObject);
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
