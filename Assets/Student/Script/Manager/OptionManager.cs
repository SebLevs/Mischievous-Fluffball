using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionManager : MonoBehaviour
{
    // SECTION - Field -------------------------------------------------------------------
                     private bool isGamePaused = false;
    
    [SerializeField] private PlayerContext playerContext;

    [SerializeField] private GameObject btnRestart;
    [SerializeField] private GameObject btnQuit;
    [SerializeField] private GameObject btnHub;

    // SECTION - Method -------------------------------------------------------------------
    private void Update()
    {
        if (playerContext.InputOption) // Toggle Option Canvas
        {
            isGamePaused = !isGamePaused;

            playerContext.InputOption = false;

            Cursor.visible = !Cursor.visible;

            btnRestart.SetActive(!btnRestart.activeSelf);
            btnQuit.SetActive(!btnQuit.activeSelf);
            btnHub.SetActive(!btnHub.activeSelf);
        }

        if (isGamePaused && !Cursor.visible)
            Cursor.visible = true;
        else if (!isGamePaused && Cursor.visible)
            Cursor.visible = false;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();  
    }

    public void GotoHub()
    {
        SceneManager.LoadScene(0);      
    }  
}
