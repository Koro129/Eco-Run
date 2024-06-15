using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float attackDamage;
    [SerializeField] private Gun gun;
    [SerializeField] private float shootInterval = 1f;
    [SerializeField] private float startShootPositionX = 0.5f;
    [SerializeField] private ObjectMovement objectMovement;
    public int currentLane;
    private float lastShootTime = 0f;

    private void Update()
    {
        if (CompareTag("Enemy"))
        {
            if (transform.position.x <= startShootPositionX)
            {
                if (Time.time >= lastShootTime + shootInterval)
                {
                    Shoot();
                    lastShootTime = Time.time;
                }
            }
        }
    }

    private void Shoot()
    {
        gun?.Shoot(currentLane);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        currentLane = objectMovement.currentLane;

        var otherScript = other.gameObject.GetComponent<PlayerController>();

        if (otherScript != null)
        {
            var otherLane = otherScript.currentLane;

            if (currentLane == otherLane && other.gameObject.CompareTag("Player"))
            {   
                if (CompareTag("Destroyable") && other.gameObject.GetComponent<PlayerController>().isSliding)
                {
                    return;
                }

                other.gameObject.GetComponent<Health>().TakeDamage(attackDamage);
            }
        }
    }
}
