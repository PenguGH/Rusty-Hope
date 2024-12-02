using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;
using Platformer.Gameplay;

public class ItemPickUp : MonoBehaviour
{
    public string itemName; // Unique identifier for the item

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object colliding is the player
        if (collision.CompareTag("Player"))
        {
            Debug.Log($"Player collided with {itemName}");
            if (!PickUpManager.Instance.HasItem(itemName))
            {
                PickUpManager.Instance.AddItem(itemName);

                // If this is the Health_Upgrade item, increase the player's max HP
                if (itemName == "Health_Upgrade")
                {
                    // Access the Health component on the player
                    var playerHealth = collision.GetComponent<Health>();
                    if (playerHealth != null)
                    {
                        playerHealth.IncreaseMaxHP(2, true); // Increase max HP by 2 and refill health
                    }
                }
                else if (itemName == "Jetpack_PickUp")
                {
                    var playerController = collision.GetComponent<PlayerController>();
                    if (playerController != null)
                    {
                        playerController.EnableDoubleJump(); // Enable double jump
                        Debug.Log("Jetpack picked up! Double jump is now enabled.");
                    }
                }

                Debug.Log($"Picked up {itemName}");
                Destroy(gameObject); // Destroy the pickup after it's collected
            }
        }
    }
}
