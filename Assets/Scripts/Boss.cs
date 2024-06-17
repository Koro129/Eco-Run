using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private GameObject groundParticle;
    [SerializeField] private Gun gun;
    [SerializeField] private float shootInterval = 0.5f; // Interval between shots
    [SerializeField] private List<Transform> bulletPoints;

    private float shootTimer;

    private void Start()
    {
        // Initialize shootTimer
        shootTimer = shootInterval;
    }

    private void Update()
    {
        // Decrement the timer
        shootTimer -= Time.deltaTime;

        // When the timer reaches zero, shoot and reset the timer
        if (shootTimer <= 0f)
        {
            Shoot();
            shootTimer = shootInterval;
        }
    }

    private void Shoot()
    {
        if (gun != null && bulletPoints.Count > 0)
        {
            var (randomBulletPoint, lane) = GetRandomBulletPoint();
            gun.Shoot(lane, randomBulletPoint.position);
        }
    }

    private (Transform bulletPoint, int lane) GetRandomBulletPoint()
    {
        int randomIndex = Random.Range(0, bulletPoints.Count);
        return (bulletPoints[randomIndex], randomIndex);
    }

    void Spawn()
    {
        Instantiate(groundParticle, new Vector3(transform.position.x, transform.position.y - 4, transform.position.z), Quaternion.Euler(-90, 0, 0));
    }
}
