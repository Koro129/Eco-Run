using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    [SerializeField] private float baseSpeed = 5f;
    [SerializeField] private float speedIncreasePerUnit = 1f; // Peningkatan kecepatan per unit progress
    [SerializeField] private float speedIncreaseInterval = 10f; // Interval untuk menambah kecepatan
    private float currentSpeed;
    public int currentLane { get; private set; }

    private void Start()
    {
        currentSpeed = baseSpeed;
        PlayerProgress.instance.OnProgressChanged += UpdateSpeed; // Subscribe ke event OnProgressChanged dari PlayerProgress
    }

    private void OnDestroy()
    {
        PlayerProgress.instance.OnProgressChanged -= UpdateSpeed; // Unsubscribe dari event OnProgressChanged saat objek dihancurkan
    }

    private void Update()
    {
        transform.Translate(Vector3.left * currentSpeed * Time.deltaTime);
    }

    private void UpdateSpeed(float progress)
    {
        // Setiap kali Progress mencapai kelipatan speedIncreaseInterval, tambahkan speedIncreasePerUnit pada kecepatan
        if (Mathf.Floor(progress) % speedIncreaseInterval == 0 && progress > 0) // Pastikan progress lebih dari 0 sebelum menambah kecepatan
        {
            currentSpeed += speedIncreasePerUnit;
            Gun gun = GetComponent<Gun>();
            if (gun != null)
            {
                gun.bulletSpeed += speedIncreasePerUnit;
            }
        }
    }

    private int GetCurrentLane(string tag)
    {
        switch (tag)
        {
            case "Ground 0":
                return 0;
            case "Ground 1":
                return 1;
            case "Ground 2":
                return 2;
            default:
                return 1; // Default to middle lane if tag is not recognized
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground 0") || other.CompareTag("Ground 1") || other.CompareTag("Ground 2"))
        {
            currentLane = GetCurrentLane(other.tag);
        }
    }
}
