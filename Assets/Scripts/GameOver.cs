using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private float gameOverDelay = 1f;

    public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RestartGame();
        }
    }

    public void Over()
    {
        Time.timeScale = 1f;
        Invoke("Show", gameOverDelay);
    }

    private void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void Show()
    {

        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }
}
