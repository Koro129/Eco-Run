using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class PlayerProgress : MonoBehaviour
{
    public static PlayerProgress instance;

    [SerializeField] private int enemiesDefeated;
    [SerializeField] private float progressIncreaseRate = 1f; // Progress increase rate per second
    // [SerializeField] private float destroyEnemiesAboveX = 10f;
    [SerializeField] private float progress; // Atur nilai x untuk menghancurkan musuh di atasnya
    public float Progress { get { return progress; } private set { progress = value; } }
    public event Action OnProgressFinished;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        enemiesDefeated = 0;
        Progress = 0f;
    }

    private void Start()
    {
        InvokeRepeating(nameof(IncreaseProgress), 1f, 1f); // Invoke IncreaseProgress every second
    }

    private void IncreaseProgress()
    {
        Progress += progressIncreaseRate;

        if (Progress >= 100f)
        {
            Debug.Log("Win!");
            // DestroyEnemies();
            CancelInvoke(nameof(IncreaseProgress));
            OnProgressFinished?.Invoke();
        }
    }

    public void EnemyDefeated(int points)
    {
        // Debug.Log("Enemy defeated!");
        enemiesDefeated++;
        if(Progress < 100f)
        {
            Progress += points;
        }
    }

    // private void DestroyEnemies()
    // {
    //     GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

    //     foreach (GameObject enemy in enemies)
    //     {
    //         if (enemy.transform.position.x > destroyEnemiesAboveX)
    //         {
    //             Destroy(enemy);
    //         }
    //     }
    // }
}
