using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    [SerializeField] private float invincibleDuration;
    [SerializeField] private float hitlagDuration;
    [SerializeField] private int point;
    [SerializeField] private GameObject destoryParticle;
    public float currentHealth { get; private set; }
    public event Action OnPlayerDeath;
    public event Action OnBossDeath;
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
                OnPlayerDeath?.Invoke();
                Hurt();
                // Destroying(); 
                Debug.Log("Game Over");
            }
            else if (CompareTag("Enemy") || CompareTag("Destroyable") || CompareTag("Coin"))
            {
                if (gameObject.layer == LayerMask.NameToLayer("Boss"))
                {
                    OnBossDeath?.Invoke();
                }

                Destroying();

                if (PlayerProgress.instance != null)
                {
                    PlayerProgress.instance.EnemyDefeated(point);
                }
            }
        }
    }

    void Hurt()
    {
        StartCoroutine(ChangeColorCoroutine());
        StartCoroutine(MakeInvincibleCoroutine());
        StartCoroutine(HitlagCoroutine());
    }

    void Destroying()
    {
        if (destoryParticle != null)
        {
            Instantiate(destoryParticle, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
    
    IEnumerator HitlagCoroutine()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(hitlagDuration);
        Time.timeScale = 1f;
        if(isDead)
        {
            Destroying();
        }
    }

    IEnumerator ChangeColorCoroutine()
    {
        GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(invincibleDuration);
        GetComponent<Renderer>().material.color = Color.white;
    }

    public IEnumerator MakeInvincibleCoroutine()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibleDuration);
        isInvincible = false;
    }
}
