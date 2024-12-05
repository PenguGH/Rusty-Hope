using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeEnemyChase : MonoBehaviour
{
    [Header("Chase Settings")]
    public Transform player;            // Reference to the player's transform
    public float chaseRange = 5f;       // Range within which the enemy will start chasing
    public float moveSpeed = 2f;        // Speed of the enemy while chasing

    [Header("Idle Behavior")]
    public float idleRange = 6f;        // Range where the enemy will stop chasing and return to idle

    private Vector3 startingPosition;   // Original position of the enemy
    private bool isChasing = false;     // Tracks if the enemy is currently chasing
    private SpriteRenderer spriteRenderer; // Reference to the enemy's SpriteRenderer

    void Start()
    {
        // Save the starting position of the enemy
        startingPosition = transform.position;

        // Cache the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer not found on this object!");
        }
    }

    void Update()
    {
        // Calculate distance to the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= chaseRange)
        {
            // Start chasing the player
            isChasing = true;
        }
        else if (distanceToPlayer >= idleRange)
        {
            // Stop chasing and return to idle
            isChasing = false;
        }

        // Perform the appropriate action
        if (isChasing)
        {
            ChasePlayer();
        }
        else
        {
            ReturnToStart();
        }
    }

    private void ChasePlayer()
{
    // Move toward the player's position
    transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

    // Flip the sprite to face the player
    if (spriteRenderer != null)
    {
        // If player is to the right, flipX = false (face right)
        spriteRenderer.flipX = player.position.x < transform.position.x;
    }
}

private void ReturnToStart()
{
    // Move back to the starting position
    transform.position = Vector3.MoveTowards(transform.position, startingPosition, moveSpeed * Time.deltaTime);

    // Flip the sprite to face the starting position
    if (spriteRenderer != null)
    {
        // If start is to the right, flipX = false (face right)
        spriteRenderer.flipX = startingPosition.x < transform.position.x;
    }
}

}
