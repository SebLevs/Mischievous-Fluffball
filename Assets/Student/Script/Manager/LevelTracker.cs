using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTracker : MonoBehaviour
{
    /// <summary : PlayerPrefs_Note>
    /// 
    /// This is the only place where Player Prefs' score / GameManager.MaxLevel are automatically updated
    ///     + Instantiation of value -> [GameManager.cs]
    ///     
    /// </summary>

    // SECTION - Method --------------------------------------------------------------------
    private void Start()
    {
        if (PlayerPrefs.GetInt("score") == 0) // On first launch of game
            PlayerPrefs.SetInt("score", GameManager.instance.MaxLevel);

        // Set current maxLevel each load of scene | Update playerprefs whenever < ActiveScene.buildIndex
        else if (GameManager.instance.MaxLevel < SceneManager.GetActiveScene().buildIndex)
        {
            GameManager.instance.MaxLevel = SceneManager.GetActiveScene().buildIndex;

            if (PlayerPrefs.GetInt("score") < GameManager.instance.MaxLevel)
                PlayerPrefs.SetInt("score", GameManager.instance.MaxLevel);
        }
        else if (GameManager.instance.MaxLevel < PlayerPrefs.GetInt("score"))
            GameManager.instance.MaxLevel = PlayerPrefs.GetInt("score");

        Debug.Log($"Max level : {GameManager.instance.MaxLevel} | Score level : {PlayerPrefs.GetInt("score")}");  
    }
}
