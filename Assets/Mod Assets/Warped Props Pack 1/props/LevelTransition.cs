using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class LevelTransition : MonoBehaviour
{
    public string nextLevelName; // Name of the next scene to load

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object entering the trigger is the player
        if (collision.CompareTag("Player"))
        {
            // Load the next scene
            SceneManager.LoadScene(nextLevelName);
        }
    }
}
