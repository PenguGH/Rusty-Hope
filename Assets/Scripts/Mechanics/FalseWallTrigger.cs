using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FalseWall : MonoBehaviour
{
    private TilemapCollider2D wallCollider;
    private Tilemap wallTilemap;

    private void Start()
    {
        // Get the Tilemap components from the current GameObject
        wallCollider = GetComponent<TilemapCollider2D>();
        wallTilemap = GetComponent<Tilemap>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player entered the false wall area.");
            // Disable the collider to allow the player to pass through
            if (wallCollider != null)
            {
                wallCollider.enabled = false;
            }

            // Make the wall semi-transparent
            if (wallTilemap != null)
            {
                wallTilemap.color = new Color(1f, 1f, 1f, 0.5f); // 50% opacity
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player exited the false wall area.");
            // Re-enable the collider to make the wall solid again
            if (wallCollider != null)
            {
                wallCollider.enabled = true;
            }

            // Reset the wall's transparency
            if (wallTilemap != null)
            {
                wallTilemap.color = new Color(1f, 1f, 1f, 1f); // Full opacity
            }
        }
    }
}
