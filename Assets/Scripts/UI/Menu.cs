using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    public Canvas mainCanvas;
    public Canvas creditsCanvas;

    public void Start()
    {
        mainCanvas.enabled = true;
        creditsCanvas.enabled = false;
    }
    public void OnPlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void OnCreditsButton()
    {
        creditsCanvas.enabled = true;
        mainCanvas.enabled = false;
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
    
    public void OnBackButton()
    {
        mainCanvas.enabled = true;
        creditsCanvas.enabled = false;
    }
}