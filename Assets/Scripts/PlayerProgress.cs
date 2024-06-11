using UnityEngine;

public class PlayerProgress : MonoBehaviour
{
    public static PlayerProgress instance;

    [SerializeField] private int enemiesDefeated;
    [SerializeField] private float progressIncreaseRate = 1f; // Progress increase rate per second
    public float Progress { get; private set; }

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
            CancelInvoke(nameof(IncreaseProgress));
        }
    }

    public void EnemyDefeated()
    {
        Debug.Log("Enemy defeated!");
        enemiesDefeated++;
        Progress += 5f;
    }
}
