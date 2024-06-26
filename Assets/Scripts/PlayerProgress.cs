using System;
using UnityEngine;

public class PlayerProgress : MonoBehaviour
{
    public static PlayerProgress instance;

    [SerializeField] private int enemiesDefeated;
    [SerializeField] private float progressIncreaseRate = 1f; // Tingkat peningkatan progress per detik
    [SerializeField] private float progress; // Nilai progress
    public float Progress { get { return progress; } private set { progress = value; } }
    public event Action<float> OnProgressChanged; // Event untuk memberi tahu perubahan progress
    public event Action OnProgressFinished; // Event untuk memberi tahu progress mencapai nilai maksimal

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
        InvokeRepeating(nameof(IncreaseProgress), 1f, 1f); // Panggil IncreaseProgress setiap detik
    }

    private void IncreaseProgress()
    {
        Progress += progressIncreaseRate;
        OnProgressChanged?.Invoke(Progress); // Panggil event OnProgressChanged untuk memberi tahu perubahan progress

        if (Progress >= 100f)
        {
            Debug.Log("Win!");
            CancelInvoke(nameof(IncreaseProgress));
            OnProgressFinished?.Invoke();
        }
    }

    public void EnemyDefeated(int points)
    {
        enemiesDefeated++;
        if (Progress < 100f)
        {
            Progress += points;
            OnProgressChanged?.Invoke(Progress); // Panggil event OnProgressChanged untuk memberi tahu perubahan progress
        }
    }
}
