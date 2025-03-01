using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Cat : MonoBehaviour
{
    [Header("Detection")]
    public float interactionRadius = 2f;
    public KeyCode interactionKey = KeyCode.E;
    
    [Header("Dialog")]
    public GameObject dialogBox;
    public TextMeshProUGUI dialogText;
    public string[] dialogLines;
    
    private int currentLine = 0;
    private bool playerInRange = false;
    private bool dialogActive = false;
    private GameObject player;
    
    void Start()
    {
        // Make sure dialog box starts inactive
        if (dialogBox != null)
            dialogBox.SetActive(false);
    }
    
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactionKey))
        {
            if (!dialogActive)
            {
                StartDialog();
            }
            else
            {
                DisplayNextLine();
            }
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            player = other.gameObject;
            
            // Optional: Show "Press E" prompt
            ShowInteractionPrompt(true);
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            player = null;
            
            // Hide the prompt and dialog if player walks away
            ShowInteractionPrompt(false);
            if (dialogActive)
                EndDialog();
        }
    }
    
    void StartDialog()
    {
        dialogActive = true;
        dialogBox.SetActive(true);
        currentLine = 0;
        DisplayDialogLine(dialogLines[currentLine]);
        
        // Optional: freeze player movement during dialog
        // PlayerController playerController = player.GetComponent<PlayerController>();
        // if (playerController != null)
        //     playerController.FreezeMovement(true);
    }
    
    void DisplayNextLine()
    {
        currentLine++;
        
        if (currentLine < dialogLines.Length)
        {
            DisplayDialogLine(dialogLines[currentLine]);
        }
        else
        {
            EndDialog();
        }
    }
    
    void DisplayDialogLine(string line)
    {
        dialogText.text = line;
    }
    
    void EndDialog()
    {
        dialogActive = false;
        dialogBox.SetActive(false);
        
        // Optional: unfreeze player movement
        // PlayerController playerController = player.GetComponent<PlayerController>();
        // if (playerController != null)
        //     playerController.FreezeMovement(false);
    }
    
    void ShowInteractionPrompt(bool show)
    {
        // Implement if you have a UI prompt
        // Example: interactionPrompt.SetActive(show);
    }
}