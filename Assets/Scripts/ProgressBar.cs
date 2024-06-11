using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private PlayerProgress playerProgress;
    [SerializeField] private Image currentProgressBar;
    private float totalProgress = 100f; // Total kemajuan yang diinginkan
    private float maxScale = 3f; // Skala maksimum yang diinginkan

    // Start is called before the first frame update
    void Start()
    {
        UpdateProgressBar();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateProgressBar();
    }

    void UpdateProgressBar()
    {
        float scale = (playerProgress.Progress / totalProgress) * maxScale;
        currentProgressBar.transform.localScale = new Vector3(scale, currentProgressBar.transform.localScale.y, currentProgressBar.transform.localScale.z);
    }
}
