using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{

    int playerLives = 3;

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
