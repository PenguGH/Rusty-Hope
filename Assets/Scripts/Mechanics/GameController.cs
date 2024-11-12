using Platformer.Core;
using Platformer.Model;
using UnityEngine;
using Cinemachine;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This class exposes the game model in the inspector and ticks the simulation.
    /// </summary>
    public class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }

        [Header("References")]
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        [SerializeField] private PlayerController player;
        [SerializeField] private Transform spawnPoint;

        // The game model that holds game state data
        public PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        void Awake()
        {
            // Ensure only one instance of GameController exists using the Singleton Pattern
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject); // Persist GameController across scenes
                Debug.Log("GameController: Initialized and set to DontDestroyOnLoad");
            }
            else if (Instance != this)
            {
                Debug.LogWarning("GameController: Duplicate instance detected, destroying...");
                Destroy(gameObject);
                return;
            }
        }

        void Start()
        {
            AssignReferences();

            // Debug log to confirm assignment to see if references are being lost due to scene transitions
            Debug.Log("GameController Start - References:");
            Debug.Log($"Virtual Camera: {(virtualCamera != null ? virtualCamera.name : "None")}");
            Debug.Log($"Player: {(player != null ? player.name : "None")}");
            Debug.Log($"Spawn Point: {(spawnPoint != null ? spawnPoint.name : "None")}");
        }

        void OnEnable()
        {
            Instance = this;
        }

        void OnDisable()
        {
            if (Instance == this) Instance = null;
        }

        void Update()
        {
            if (Instance == this) Simulation.Tick();
        }

        /// <summary>
        /// Assigns the missing references if they are not set in the inspector.
        /// </summary>
        private void AssignReferences()
        {
            if (virtualCamera == null)
            {
                virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
                Debug.Log(virtualCamera != null 
                    ? $"Virtual Camera found: {virtualCamera.name}" 
                    : "Virtual Camera not found!");
            }

            if (player == null)
            {
                player = FindObjectOfType<PlayerController>();
                Debug.Log(player != null 
                    ? $"Player found: {player.name}" 
                    : "Player not found!");
            }

            if (spawnPoint == null)
            {
                GameObject spawnObj = GameObject.Find("SpawnPoint");
                spawnPoint = spawnObj != null ? spawnObj.transform : null;
                Debug.Log(spawnPoint != null 
                    ? $"Spawn Point found: {spawnPoint.name}" 
                    : "Spawn Point not found!");
            }

            // Assign model references if not already assigned
            if (model.player == null && player != null)
                model.player = player;

            if (model.virtualCamera == null && virtualCamera != null)
                model.virtualCamera = virtualCamera;

            if (model.spawnPoint == null && spawnPoint != null)
                model.spawnPoint = spawnPoint;
        }
    }
}