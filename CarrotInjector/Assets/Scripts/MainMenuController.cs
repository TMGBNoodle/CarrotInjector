using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public string gameSceneName = "Game"; // main game scene
    public string creditsSceneName = "Credits"; // credits scene
    
    // Reference to controls panel that will be toggled
    public GameObject controlsPanel;
    
    private void Start()
    {
        // Hide controls panel on start
        if (controlsPanel != null)
            controlsPanel.SetActive(false);
    }
    
    // Called by Play button
    public void PlayGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }
    
    // Called by Controls button
    public void ShowControls()
    {
        if (controlsPanel != null)
            controlsPanel.SetActive(true);
    }
    
    // Called by back button in controls panel
    public void HideControls()
    {
        if (controlsPanel != null)
            controlsPanel.SetActive(false);
    }
    
    // Called by Credits button
    public void ShowCredits()
    {
        SceneManager.LoadScene(creditsSceneName);
    }
    
}
