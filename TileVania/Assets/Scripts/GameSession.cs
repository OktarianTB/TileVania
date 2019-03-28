using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    int playerLives = 3;
    int playerScore = 0;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI livesText; 

    private void Awake()
    {
        int nbGameSessions = FindObjectsOfType<GameSession>().Length;
        if(nbGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        if(!scoreText || !livesText)
        {
            Debug.LogWarning("One or more of the TextMeshProUGUIs is missing");
            return;
        }

        livesText.text = playerLives.ToString();
        scoreText.text = playerScore.ToString();
    }

    public void AddToScore(int score)
    {
        playerScore += score;
        scoreText.text = playerScore.ToString();
    }

    public void ProcessPlayerDamage()
    {
        playerLives--;
        if(playerLives <= 0)
        {
            ResetGameSession();
        }
        else
        {
            livesText.text = playerLives.ToString();
            int currentScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentScene);
        }
    }

    private void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
