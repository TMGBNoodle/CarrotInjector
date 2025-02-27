using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    public GameObject playerSpawn;

    public GameObject playerPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

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
        GameObject newPlayer = Instantiate(playerPrefab);
        Vector3 spawnPos = playerSpawn.transform.position;
        newPlayer.transform.position = new Vector3(spawnPos.x, spawnPos.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
