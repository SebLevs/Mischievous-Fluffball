using UnityEngine;

public class TeleportStatue : MonoBehaviour
{
    // SECTION - Field --------------------------------------------------------------------
    [SerializeField] private PlayerContext player;
    [SerializeField] private Transform tpTr;

    public Transform TpTr { get => tpTr; set => tpTr = value; }




    // SECTION - Method --------------------------------------------------------------------
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && player.Rb.velocity.magnitude == 0)
            player.transform.position = new Vector2(tpTr.position.x, tpTr.position.y);
    }
}
