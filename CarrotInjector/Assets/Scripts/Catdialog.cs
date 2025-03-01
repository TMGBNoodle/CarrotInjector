using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CatDialog : MonoBehaviour
{
    [Header("Dialog Settings")]
    [TextArea(3, 10)]
    public string[] dialogLines; // Array of dialog lines
    public float interactionRadius = 2f; // How close player needs to be
    
    [Header("UI References")]
    public GameObject dialogPanel; // Your GUI dialog background
    public TextMeshProUGUI dialogText; // Text component for dialog
    public Image catPortrait; // Optional: cat portrait image
    public Sprite catImage; // Optional: image of the cat for the dialog
    
    [Header("Controls")]
    public KeyCode interactionKey = KeyCode.E;
    
    // Private variables
    private bool playerInRange = false;
    private bool dialogActive = false;
    private int currentLine = 0;
    private GameObject player;
    private Collider2D triggerCollider;
    
    void Start()
    {

        // Ensure dialog panel and all related UI elements are hidden at start
        if (dialogPanel != null)
            dialogPanel.SetActive(false);
            
        // Explicitly hide the portrait at start, even if it's not a child of the dialog panel
        if (catPortrait != null)
            catPortrait.gameObject.SetActive(false);
            
        // Explicitly hide the dialog text as well
        if (dialogText != null)
            dialogText.gameObject.SetActive(false);
            
        // Ensure dialog is hidden at start
        if (dialogPanel != null)
            dialogPanel.SetActive(false);
        
        // Optional: Set cat portrait if provided
        if (catPortrait != null && catImage != null)
            catPortrait.sprite = catImage;
            
        // Get trigger collider
        triggerCollider = GetComponent<Collider2D>();
        if (triggerCollider != null && !triggerCollider.isTrigger)
            Debug.LogWarning("Cat collider should be set as a trigger!");
    }
    
    void Update()
    {
        // Find player if we don't have one yet
        if (player == null && GameManager.Instance != null)
        {
            player = GameManager.Instance.GetCurrentPlayer();
            if (player != null)
                CheckPlayerDistance();
        }
        
        // Check for interaction or dialog continuation
        if (playerInRange && Input.GetKeyDown(interactionKey))
        {
            if (!dialogActive)
                StartDialog();
            else
                DisplayNextLine();
        }
        
        // Update player distance check every frame
        if (player != null)
            CheckPlayerDistance();
    }
    
    void CheckPlayerDistance()
    {
        // Manual distance check in case triggers aren't working as expected
        float distance = Vector2.Distance(transform.position, player.transform.position);
        bool wasInRange = playerInRange;
        playerInRange = distance <= interactionRadius;
        
        // Handle range changes
        if (playerInRange && !wasInRange)
            ShowInteractionPrompt(true);
        else if (!playerInRange && wasInRange)
        {
            ShowInteractionPrompt(false);
            if (dialogActive)
                EndDialog();
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if player entered detection zone
        if (other.gameObject == GameManager.Instance.GetCurrentPlayer())
        {
            playerInRange = true;
            player = other.gameObject;
            ShowInteractionPrompt(true);
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        // Check if player left detection zone
        if (other.gameObject == player)
        {
            playerInRange = false;
            ShowInteractionPrompt(false);
            
            if (dialogActive)
                EndDialog();
        }
    }
    
    void StartDialog()
    {
        dialogActive = true;
        
        // Show the dialog panel
        if (dialogPanel != null)
            dialogPanel.SetActive(true);
        
        // Also make sure to show the text and portrait
        if (dialogText != null && dialogText.gameObject != dialogPanel)
            dialogText.gameObject.SetActive(true);
            
        if (catPortrait != null && catPortrait.gameObject != dialogPanel)
            catPortrait.gameObject.SetActive(true);
        
        currentLine = 0;
        
        // Set the text
        if (dialogLines != null && dialogLines.Length > 0)
            dialogText.text = dialogLines[currentLine];
    }
        
    void DisplayNextLine()
    {
        currentLine++;
        
        if (currentLine < dialogLines.Length)
            dialogText.text = dialogLines[currentLine];
        else
            EndDialog();
    }
    
    void EndDialog()
    {
        dialogActive = false;
        
        // Hide the dialog panel
        if (dialogPanel != null)
            dialogPanel.SetActive(false);
        
        // Also make sure to hide the text and portrait if they're separate from the panel
        if (dialogText != null && dialogText.gameObject != dialogPanel)
            dialogText.gameObject.SetActive(false);
            
        if (catPortrait != null && catPortrait.gameObject != dialogPanel)
            catPortrait.gameObject.SetActive(false);
    }
    
    void ShowInteractionPrompt(bool show)
    {
        // If you have an interaction prompt UI element, activate it here
        // Example: interactionPromptUI.SetActive(show);
    }
    
    // Draw the interaction radius in the editor for debugging
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}