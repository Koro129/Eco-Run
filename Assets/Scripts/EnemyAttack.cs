using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float attackDamage;
    [SerializeField] private Gun gun;

    [SerializeField] private float shootInterval = 1f;

    private void Start()
    {
        if (CompareTag("Enemy"))
        {
            InvokeRepeating(nameof(Shoot), shootInterval, shootInterval);
        }
    }

    private void Shoot()
    {
        gun?.Shoot();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (CompareTag("Destroyable") && collision.gameObject.GetComponent<PlayerController>().isSliding)
            {
                return;
            }

            collision.gameObject.GetComponent<Health>().TakeDamage(attackDamage);
            Debug.Log("Attacking object: " + collision.gameObject.name);
        }
    }
}
