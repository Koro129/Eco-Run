using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float rateOfFire = 1f;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float range = 10f;
    [SerializeField] private int damage = 10;
    [SerializeField] private Transform bulletPoint;
    [SerializeField] private bool shootRight = true;

    private float lastShootTime = 0f;

    public void Shoot()
    {
        if (Time.time >= lastShootTime + (1f / rateOfFire))
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletPoint.position, Quaternion.identity);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.Initialize(range, damage, bulletSpeed, shootRight);
                Vector2 direction = shootRight ? Vector2.right : Vector2.left;
                bulletScript.SetDirection(direction);
            }
            lastShootTime = Time.time;
        }
    }
}
