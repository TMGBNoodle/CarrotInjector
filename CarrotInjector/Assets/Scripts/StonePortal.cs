using UnityEngine;
using UnityEngine.SceneManagement;

public class StonePortal : MonoBehaviour
{
    public string creditsSceneName = "Credits"; // The name of credits scene
    public float interactionRadius = 2f;
    public KeyCode interactionKey = KeyCode.E;
    
    private bool playerInRange = false;
    private GameObject player;
    
    void Update()
    {
        // Find player if needed
        if (player == null && GameManager.Instance != null)
            player = GameManager.Instance.GetCurrentPlayer();
            
        // Check for interaction when player is in range
        if (playerInRange && Input.GetKeyDown(interactionKey))
        {
            LoadCreditsScene();
        }
            
        // Update player distance check
        if (player != null)
            CheckPlayerDistance();
    }
    
    void CheckPlayerDistance()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        playerInRange = distance <= interactionRadius;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == GameManager.Instance.GetCurrentPlayer())
        {
            playerInRange = true;
            player = other.gameObject;
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
        }
    }
    
    void LoadCreditsScene()
    {
        SceneManager.LoadScene(creditsSceneName);
    }
    
    // Draw the interaction radius in the editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}
