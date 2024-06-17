using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    [SerializeField] private GameObject finishUI;

    public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("SceneMenu");
        }
    }
    
    public void Show()
    {

        finishUI.SetActive(true);
        Time.timeScale = 0f;
    }
}
