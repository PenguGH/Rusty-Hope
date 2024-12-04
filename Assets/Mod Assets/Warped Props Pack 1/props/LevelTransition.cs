using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class LevelTransition : MonoBehaviour
{
    public string nextLevelName; // Name of the next scene to load

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object entering the trigger is the player
        Debug.Log("Collision detected with object: " + collision.name);

        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player detected. Loading next level: " + nextLevelName);

            // Check if the next scene exists before loading
            if (SceneUtility.GetBuildIndexByScenePath(nextLevelName) != -1)
            {
                // Load the next scene
                SceneManager.LoadScene(nextLevelName);
            }
            else
            {
                Debug.LogError("Scene '" + nextLevelName + "' does not exist or is not added to the build settings.");
            }
        }
        else
        {
            Debug.LogWarning("Non-player object collided with transition: " + collision.name);
        }
    }
}