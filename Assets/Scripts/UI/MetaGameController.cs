// Debugging code for UI Canvas
using Platformer.Mechanics;
using Platformer.UI;
using UnityEngine;

namespace Platformer.UI
{   /// <summary>
    /// The MetaGameController is responsible for switching control between the high level
    /// contexts of the application, eg the Main Menu and Gameplay systems.
    /// </summary>
    public class MetaGameController : MonoBehaviour
    {
        /// <summary>
        /// The main UI object which used for the menu.
        // Reference to the Main UI Controller (menu)
        /// </summary>
    
        // Reference to the Main UI Controller (menu)
        public MainUIController mainMenu;
        

        /// <summary>
        /// A list of canvas objects which are used during gameplay (when the main ui is turned off)
        // Array of canvas objects for gameplay (these are activated/deactivated when toggling the main menu)
        /// </summary>
        // Array of canvas objects for gameplay (these are activated/deactivated when toggling the main menu)
        public Canvas[] gamePlayCanvasii;
        
        /// <summary>
        /// The game controller.
        // Reference to the Game Controller
        /// </summary>
        public GameController gameController;

        // Boolean to track if the main menu is currently shown
        private bool showMainCanvas = false;

        // Called when the script is enabled (first frame or re-enabled)
        void OnEnable()
        {
            Debug.Log("OnEnable called");
            _ToggleMainMenu(showMainCanvas);  // Initially set the main menu state based on showMainCanvas
        }

        /// <summary>
        /// Turn the main menu on or off.
        // Toggle the main menu visibility and pause/unpause the game
        /// </summary>
        /// <param name="show"></param>
        public void ToggleMainMenu(bool show)
        {
            Debug.Log("Toggling Main Menu. Show: " + show);

            // Only toggle if the current state is different from the desired state
            if (this.showMainCanvas != show)
            {
                _ToggleMainMenu(show);
            }
        }

        // Internal method to toggle the main menu and time scale (pause/unpause the game)
        void _ToggleMainMenu(bool show)
        {
            // Check if mainMenu is null
            if (mainMenu == null)
            {
                Debug.LogError("Main Menu is not assigned!");
                return;
            }

            if (show)
            {
                Debug.Log("Game Paused");
                Time.timeScale = 0.0001f;  // Freeze the game but allow input (time scale set to a very small value)
                mainMenu.gameObject.SetActive(true);  // Show the main menu UI
                Debug.Log("Main Menu Set Active");

                // Deactivate all gameplay canvases
                foreach (var i in gamePlayCanvasii)
                {
                    if (i == null)
                    {
                        Debug.LogError("One of the GamePlayCanvasii is not assigned!");
                        continue;  // Skip this entry if it's null
                    }

                    i.gameObject.SetActive(false);
                    Debug.Log(i.name + " Deactivated");
                }
            }
            else
            {
                Debug.Log("Game Resumed");
                Time.timeScale = 1;  // Unfreeze the game (set time scale back to normal)
                mainMenu.gameObject.SetActive(false);  // Hide the main menu UI
                Debug.Log("Main Menu Set Inactive");

                // Activate all gameplay canvases
                foreach (var i in gamePlayCanvasii)
                {
                    if (i == null)
                    {
                        Debug.LogError("One of the GamePlayCanvasii is not assigned!");
                        continue;  // Skip this entry if it's null
                    }

                    i.gameObject.SetActive(true);
                    Debug.Log(i.name + " Activated");
                }
            }

            this.showMainCanvas = show;  // Update the state of whether the main menu is shown
        }

        // Called every frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))  // Check if Escape key is pressed
            {
                Debug.Log("Escape Key Pressed");
                ToggleMainMenu(show: !showMainCanvas);  // Toggle the main menu visibility
            }
        }
    }
}