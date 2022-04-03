using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // SECTION - Field -------------------------------------------------------------------
    public static GameManager instance = null; // Singleton

    [SerializeField] private int maxLevel = 1;


    // SECTION - Field -------------------------------------------------------------------
    public int MaxLevel { get => maxLevel; set => maxLevel = value; }


    // SECTION - Method - General -------------------------------------------------------------------
    private void Awake()
    {
        // singleton
        
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // To keep track of MaxLevel
        }
        else if (instance != this)
            Destroy(gameObject);

        // Load higher scene reached
        GameManager.instance.MaxLevel = PlayerPrefs.GetInt("score");

        // Mouse Management
        Cursor.visible = false;
    }

    #region REGION - DEVTOOL : RESET PLAYER PREFS

    [SerializeField] private bool resetScore = false; // Debugger field for devtool
    [SerializeField] private bool deleteScore = false; // Debugger field for devtool
    private void Update()
    {
        /// <sumary : How_To_Use>
        ///      - NOTE : Reset PlayerPrefs for Test purposes
        ///             + resetScore = GameManager.instance.MaxLevel (should be 1)
        ///             + deleteScore = 0
        ///      - Must manually uncheck [deleteScore] bool throught inspector while in PlayMode
        /// </sumary>
        
        if (resetScore)
        {
            // Reset maxLevel & pp_score
            GameManager.instance.MaxLevel = 1;
            PlayerPrefs.SetInt("score", GameManager.instance.MaxLevel);
            
            // toggle so that ssd does not implode
            resetScore = false;

            Debug.Log($"New MaxLevel : {GameManager.instance.MaxLevel} | new player pref score : {PlayerPrefs.GetInt("score")}");
        }
        else if (deleteScore)
        {
            // Reset maxLevel & pp_score
            GameManager.instance.MaxLevel = 1;
            PlayerPrefs.DeleteKey("score");

            // toggle so that ssd does not implode
            deleteScore = false;

            Debug.Log($"New MaxLevel : {GameManager.instance.MaxLevel} | new player pref score : {PlayerPrefs.GetInt("score")}");
        }
    }
    #endregion
}
