using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    public GameObject playerSpawn;
    public GameObject playerPrefab;
    public CameraFollow cameraFollow; // Add reference to camera follow script
    
    private GameObject currentPlayer; // Track the current player instance

    void Awake()
    {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        spawnPlayer();
    }

    public void spawnPlayer() {
        // Clean up any existing player first
        if (currentPlayer != null) {
            Destroy(currentPlayer);
        }
        
        // Spawn the new player
        currentPlayer = Instantiate(playerPrefab);
        Vector3 spawnPos = playerSpawn.transform.position;
        currentPlayer.transform.position = new Vector3(spawnPos.x, spawnPos.y, 0);
        
        // Set the camera to follow the new player
        if (cameraFollow != null) {
            cameraFollow.target = currentPlayer.transform;
        } else {
            Debug.LogWarning("Camera Follow script not assigned in GameManager!");
        }
    }
    
    public void RespawnPlayer() {
        spawnPlayer();
    }

    // This allows you to access the player from other scripts if needed
    public GameObject GetCurrentPlayer() {
        return currentPlayer;
    }

    void Update()
    {
        // Your existing update logic
    }
}