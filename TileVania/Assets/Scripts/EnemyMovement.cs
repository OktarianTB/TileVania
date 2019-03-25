using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    float moveSpeed = 0.5f;
    Rigidbody2D rigibody;

    void Start()
    {
        rigibody = GetComponent<Rigidbody2D>();

        if (!rigibody)
        {
            Debug.LogWarning("Rigidbody 2D component is missing from enemy prefab");
        }
    }
    
    void Update()
    {
        if (!rigibody)
        {
            Debug.LogWarning("Update isn't being called: an error as been detected");
            return;
        }

        MoveEnemy();

    }

    private void MoveEnemy()
    {
        if (IsFacingRight())
        {
            Vector2 enemyVelocity = new Vector2(moveSpeed, 0f);
            rigibody.velocity = enemyVelocity;
        }
        else
        {
            Vector2 enemyVelocity = new Vector2(-moveSpeed, 0f);
            rigibody.velocity = enemyVelocity;
        }
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    private void FlipEnemy()
    {
        transform.localScale = new Vector2(-Mathf.Sign(rigibody.velocity.x), 1f);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        FlipEnemy();
    }


}
