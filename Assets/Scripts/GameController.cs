using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState { Running, Pause, GameOver, Finish };

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject destroyer;
    [SerializeField] GameObject boss;
    [SerializeField] float bossDelay = 10f;
    [SerializeField] float finishDelay = 10f;
    public PlayerController playerController;
    public PauseMenu pauseMenu;
    public GameOver gameOver;
    public Finish finish;
    public GameState state;

    private void Start()
    {
        pauseMenu.OnPause += () =>
        {
            state = GameState.Pause;
        };

        pauseMenu.OnResume += () =>
        {
            if(state == GameState.Pause)
            {
                state = GameState.Running;
            }
        };

        PlayerProgress.instance.OnProgressFinished += () =>
        {
            destroyer.SetActive(true);
            StartCoroutine(SpawnBossDelayed());
        };

        Health healthComponent = playerController.GetComponent<Health>();

        if (healthComponent != null)
        {
            healthComponent.OnPlayerDeath += () =>
            {
                state = GameState.GameOver;
                gameOver.Over();
            };
        }

        Health bossHealthComponent = boss.GetComponent<Health>();
        
        if (bossHealthComponent != null)
        {
            bossHealthComponent.OnBossDeath += () =>
            {
                StartCoroutine(FinishDelayed());
            };
        }
    }

    private void Update()
    {
        if (state == GameState.Running)
        {
            playerController.HandleInput();
        }
        else if(state == GameState.Pause)
        {
            Debug.Log("Game is paused");   
        }
        else if(state == GameState.GameOver)
        {
            gameOver.HandleInput();
        }
        else if(state == GameState.Finish)
        {
            finish.HandleInput();
        }
    }


    private IEnumerator SpawnBossDelayed()
    {
        yield return new WaitForSeconds(bossDelay);
        SpawnBoss();
    }

    private void SpawnBoss()
    {
        boss?.SetActive(true);
        Debug.Log("Boss spawned!");
    }

    private IEnumerator FinishDelayed()
    {
        yield return new WaitForSeconds(finishDelay);
        state = GameState.Finish;
        finish.Show();
    }
}
