using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    // Config
    [SerializeField] private float runSpeed = 5f;
    [SerializeField] private float jumpSpeed = 10f;
    [SerializeField] private float climbSpeed = 2f;
    [SerializeField] private Vector2 deathKick = new Vector2(5f, 25f);

    // State
    bool isAlive = true;

    // Cached component references
    Rigidbody2D rigibody;
    Animator animator;
    CapsuleCollider2D capsuleCollider;
    BoxCollider2D boxCollider;
    private float initialGravity;

    void Start()
    {
        rigibody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        boxCollider = GetComponent<BoxCollider2D>();

        if (!rigibody)
        {
            Debug.LogWarning("Rigidbody2D component is missing on player");
        }
        if (!animator)
        {
            Debug.LogWarning("Animator component is missing on player");
        }
        if (!capsuleCollider)
        {
            Debug.LogWarning("Capsule Collider2D component is missing on player");
        }
        if (!boxCollider)
        {
            Debug.LogWarning("Box Collider2D component is missing on player");
        }

        initialGravity = rigibody.gravityScale;
    }
    
    void Update()
    {
        if (!rigibody || !animator || !capsuleCollider || !boxCollider)
        {
            Debug.LogWarning("Update isn't being called: an error has been detected");
            return;
        }

        if (!isAlive)
        {
            return;
        }

        Run();
        Jump();
        ClimbLadder();
        FlipSprite();
        Die();
    }

    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); // value is between -1 to +1
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, rigibody.velocity.y);
        rigibody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(rigibody.velocity.x) > Mathf.Epsilon;
        animator.SetBool("IsRunning", playerHasHorizontalSpeed);
    }

    private void Jump()
    {
        bool playerIsOnGround = boxCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
 
        if (CrossPlatformInputManager.GetButtonDown("Jump") && playerIsOnGround)
        {
            Vector2 jumpVelocity = new Vector2(0f, jumpSpeed);
            rigibody.velocity += jumpVelocity;
        }
    }

    private void ClimbLadder()
    {
        bool playerIsOnLadder = capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"));

        if (playerIsOnLadder)
        {
            float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
            Vector2 climbVelocity = new Vector2(rigibody.velocity.x, climbSpeed * controlThrow);
            rigibody.velocity = climbVelocity;

            rigibody.gravityScale = 0;

            bool playerHasVerticalSpeed = Mathf.Abs(rigibody.velocity.y) > Mathf.Epsilon;
            animator.SetBool("IsClimbing", playerHasVerticalSpeed);
        } else
        {
            animator.SetBool("IsClimbing", false);
            rigibody.gravityScale = initialGravity;
        }

        
    }

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rigibody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rigibody.velocity.x), 1f);
        }
    }

    private void Die()
    {
        if (capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            isAlive = false;
            animator.SetTrigger("Dying");
            rigibody.velocity = deathKick;
        }
    }


}
