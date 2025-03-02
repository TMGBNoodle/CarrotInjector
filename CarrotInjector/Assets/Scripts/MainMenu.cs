using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour
{
    public string mainMenuScene = "MainMenu";
    
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }
}