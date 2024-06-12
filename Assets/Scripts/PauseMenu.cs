using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool isPaused;
    public event Action OnPause;
    public event Action OnResume;

    private void Start()
    {
        pauseMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    private void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        OnPause?.Invoke();
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        OnResume?.Invoke();
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        OnResume?.Invoke();
        SceneManager.LoadScene("Level1");
        
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("SceneMenu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
