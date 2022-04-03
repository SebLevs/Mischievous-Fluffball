using UnityEngine;

public class QuitStatue : MonoBehaviour
{ 
    // SECTION - Field --------------------------------------------------------------------
    [SerializeField] private PlayerContext player;


    // SECTION - Method --------------------------------------------------------------------
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && player.Rb.velocity.magnitude == 0)
            Application.Quit();
    }
}
