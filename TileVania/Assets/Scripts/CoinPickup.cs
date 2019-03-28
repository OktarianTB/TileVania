using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{

    [SerializeField] AudioClip coinPickup;
    [SerializeField] int pointsPerCoin = 10;

    GameSession gameSession;

    private void Start()
    {
        gameSession = FindObjectOfType<GameSession>();

        if (!gameSession)
        {
            Debug.LogWarning("Game Session object hasn't been found");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(coinPickup, Camera.main.transform.position);
        gameSession.AddToScore(pointsPerCoin);
        Destroy(gameObject);
    }

}
