using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class LevelTransition : MonoBehaviour
{
    public string nextLevelName; // Name of the next scene to load
    private bool isPlayerInTrigger = false; // Tracks if the player is in the trigger

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object entering the trigger is the player
        if (collision.CompareTag("Player"))
        {
            isPlayerInTrigger = true; // Player is in the trigger zone
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Check if the object exiting the trigger is the player
        if (collision.CompareTag("Player"))
        {
            isPlayerInTrigger = false; // Player has left the trigger zone
        }
    }

    private void Update()
    {
        // Check if the player is in the trigger zone and presses "Up Arrow" or "W"
        if (isPlayerInTrigger && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)))
        {
            // Load the next scene
            SceneManager.LoadScene(nextLevelName);
        }
    }
}
