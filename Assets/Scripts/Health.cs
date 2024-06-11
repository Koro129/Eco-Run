using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    [SerializeField] private float invincibleDuration;
    public float currentHealth { get; private set; }
    private bool isDead;
    private bool isInvincible;

    void Awake()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(float _damage)
    {
        if (!isInvincible)
        {
            currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
            if (currentHealth > 0)
            {
                Hurt();
            }
            else
            {
                Die();
            }
        }
    }

    void Die()
    {
        if (!isDead)
        {
            isDead = true;

            if (CompareTag("Player"))
            {
                // Perilaku ketika player mati
                Debug.Log("Game Over");
                // Tambahkan logika game over di sini
            }
            else if (CompareTag("Enemy"))
            {
                // Perilaku ketika enemy mati
                Destroy(gameObject);

                // Beritahu PlayerProgress bahwa musuh telah mati
                if (PlayerProgress.instance != null)
                {
                    PlayerProgress.instance.EnemyDefeated();
                }
            }
        }
    }

    void Hurt()
    {
        StartCoroutine(ChangeColorCoroutine());
        StartCoroutine(MakeInvincibleCoroutine());
    }

    IEnumerator ChangeColorCoroutine()
    {
        GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(1f);
        GetComponent<Renderer>().material.color = Color.white;
    }

    IEnumerator MakeInvincibleCoroutine()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibleDuration);
        isInvincible = false;
    }
}
