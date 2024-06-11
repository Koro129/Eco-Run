using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapScene : MonoBehaviour
{
    public string sceneName;
    public GameObject level1Text; // Reference to the text GameObject
    private bool isPlayerInTrigger = false;

    private void Start()
    {
        if (level1Text != null)
        {
            level1Text.SetActive(false); // Make sure the text is initially hidden
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
            print("Player Entered Trigger Area");

            if (level1Text != null)
            {
                level1Text.SetActive(true); // Show the text when player enters the trigger area
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
            print("Player Exited Trigger Area");

            if (level1Text != null)
            {
                level1Text.SetActive(false); // Hide the text when player exits the trigger area
            }
        }
    }

    private void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
    }
}
