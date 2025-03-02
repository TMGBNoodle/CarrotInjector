using UnityEngine;

public class DeathZone : MonoBehaviour 
{
    public float deathY = -25f; // Y position below which player dies
    
    void Update()
    {
        if (GameManager.Instance == null) return;
        
        GameObject player = GameManager.Instance.GetCurrentPlayer();
        if (player == null) return;
        
        // Check if player is below death line
        if (player.transform.position.y < deathY)
        {
            Debug.Log("Player fell below death line - respawning!");
            GameManager.Instance.RespawnPlayer();
        }
    }
}