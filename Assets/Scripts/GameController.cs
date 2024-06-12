using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public enum GameState { Running, Pause };
public class GameController : MonoBehaviour
{
    public PlayerController playerController;
    public PauseMenu pauseMenu;
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
        
    }
}