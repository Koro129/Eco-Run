using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float attackDamage;
    [SerializeField] private Gun gun;
    [SerializeField] private float shootInterval = 1f;
    [SerializeField] private ObjectMovement objectMovement;
    public int currentLane;

    private void Start()
    {
        if (CompareTag("Enemy"))
        {
            InvokeRepeating(nameof(Shoot), shootInterval, shootInterval);
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
                Debug.Log("Player at lane " + currentLane + " is hit by " + gameObject.name + " at lane " + otherLane);
                
                if (CompareTag("Destroyable") && other.gameObject.GetComponent<PlayerController>().isSliding)
                {
                    return;
                }

                other.gameObject.GetComponent<Health>().TakeDamage(attackDamage);
            }
        }
    }
}
