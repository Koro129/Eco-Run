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

    // private void Update()
    // {
    //     if(Input.GetKeyDown(KeyCode.E))
    //     {
    //         TakeDamage(1);
    //     }
    // }
    // Start is called before the first frame update
    public void TakeDamage(float _damage)
    {
        if(!isInvincible)
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
            // Game Over
        }
    }

    void Hurt()
    {
        StartCoroutine(ChangeColorCoroutine());
        StartCoroutine(MakeInvincibleCoroutine());
    }

    IEnumerator ChangeColorCoroutine()
    {
        // Change color to red
        GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(1f);
        // Change color back to original
        GetComponent<Renderer>().material.color = Color.white;
    }

    IEnumerator MakeInvincibleCoroutine()
    {
        // Set invincible to true
        isInvincible = true;
        yield return new WaitForSeconds(invincibleDuration);
        // Set invincible back to false
        isInvincible = false;
    }
}
