using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStatue : MonoBehaviour
{
    // SECTION - Field --------------------------------------------------------------------
    [SerializeField] private PlayerContext player;

    [SerializeField] private GameObject requiredObject;
    [SerializeField] private ParticleSystem psVisualCue;

    [SerializeField] private int sceneIndexRef = 1;


    // SECTION - Method --------------------------------------------------------------------
    private void Start()
    {
        // Play() PS for available level in main menu --- else --- Play() PS
        if (sceneIndexRef <= GameManager.instance.MaxLevel && SceneManager.GetActiveScene().buildIndex == 0) // GameManager.instance.MaxLevel
            psVisualCue.Play();
        else if (SceneManager.GetActiveScene().buildIndex != 0) // Play in levels
            psVisualCue.Play();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // In Main menu
        if (SceneManager.GetActiveScene().buildIndex == 0 && other.gameObject == requiredObject)
        {
            if (sceneIndexRef <= PlayerPrefs.GetInt("score") && player.Rb.velocity.magnitude == 0) // PlayerPrefs.GetInt("score")
                SceneManager.LoadScene(sceneIndexRef);
        }
        // In Levels
        else
           if (other.gameObject.name.Contains(requiredObject.name) && player.Rb.velocity.magnitude == 0)
                SceneManager.LoadScene(sceneIndexRef);
    }
}
