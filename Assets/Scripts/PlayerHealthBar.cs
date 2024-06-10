using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image currentHealthBar;
    // Start is called before the first frame update
    void Start()
    {
        currentHealthBar.transform.localScale = new Vector3(playerHealth.currentHealth / 10f * 2f, currentHealthBar.transform.localScale.y, currentHealthBar.transform.localScale.z);
        
    }

    // Update is called once per frame
    void Update()
    {
        currentHealthBar.transform.localScale = new Vector3(playerHealth.currentHealth / 10f * 2f, currentHealthBar.transform.localScale.y, currentHealthBar.transform.localScale.z);

    }
}
