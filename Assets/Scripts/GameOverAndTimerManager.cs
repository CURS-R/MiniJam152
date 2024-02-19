using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class GameOverAndTimerManager : MonoBehaviour
{
    // Externals
    public GameObject GameOverUI;
    public TextMeshProUGUI TimerText;
    
    // Internals
    private float timer = 0f;
    private bool initialized = false;
    private bool isGameOver = false;

    private PlayerScript playerScript;
    private GameManager gameManager;

    private void Awake()
    {
        // Made this in the last minutes of the game jam, please don't hate this
        playerScript = FindObjectOfType<PlayerScript>();
        gameManager = FindObjectOfType<GameManager>();
        initialized = true;
        ToggleGameOver(false);
    }

    private void Update()
    {
        // Update timer if game is not over
        if (!isGameOver)
        {
            timer += Time.deltaTime; // Increment timer by the time between frames
            UpdateTimerDisplay(); // Update the timer display

            // Check for game over condition
            if (gameManager.Cheeses.Count <= 0)
            {
                ToggleGameOver(true);
                isGameOver = true; // Stop the timer
            }
        }
    }

    private void LateUpdate()
    {
        UpdateTimerDisplay();
        if (gameManager.Cheeses.Count <= 0)
        {
            ToggleGameOver(true);
        }
    }
    
    private void UpdateTimerDisplay()
    {
        // Convert timer to minutes and seconds, then update the TimerText
        var timeSpan = TimeSpan.FromSeconds(timer);
        TimerText.text = $"{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
    }

    private void ToggleGameOver(bool gameOver)
    {
        isGameOver = gameOver;
        GameOverUI.SetActive(gameOver);
    }
}
