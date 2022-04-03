using UnityEngine;


/// <summary : How_To_Use>
/// 
/// ElevatorA is the [context]
/// [context]'s velocity is mirrored by [receiver] in the specified direction at [receiverDir]
/// [Context] elevator ONLY moves on the XY axis, as of right now
/// 
/// Current movement pattern implemented for receiver:
///     - NONE : No velocity applied
///     - DOWNUP : Goes Down then Up
///     - UDOWN : Goes Up then Down
///     - LEFTRIGHT : Goes Left then Right
///     - RIGHTLEFT : Goes Right then Left
/// 
/// </summary>

public enum VelocityDirection { NONE, DOWNUP, UPDOWN, LEFTRIGHT, RIGHTLEFT}

public class SimpleElevator : MonoBehaviour
{
    // SECTION - Field --------------------------------------------------------------------
    [Header("Context & Receiver Rigidbodies")]
    [SerializeField] private Rigidbody2D contextRb;
    [SerializeField] private Rigidbody2D receiverRb;

    [Header("Boundaries")]
    [SerializeField] private Collider2D limitRoof;
    [SerializeField] private Collider2D limitGround;

    [Header("Receiver Direction")]
    [SerializeField] private VelocityDirection receiverDir = VelocityDirection.UPDOWN;


    // SECTION - Method --------------------------------------------------------------------
    private void FixedUpdate()
    {
        if (receiverRb.bodyType != RigidbodyType2D.Static)
            switch(receiverDir)
            {
                case VelocityDirection.DOWNUP:
                    receiverRb.velocity = contextRb.velocity;
                    break;

                case VelocityDirection.UPDOWN:
                    receiverRb.velocity = contextRb.velocity * -1;
                    break;

                case VelocityDirection.LEFTRIGHT:
                    receiverRb.velocity = new Vector2(contextRb.velocity.y, contextRb.velocity.x);
                    break;

                case VelocityDirection.RIGHTLEFT:
                    receiverRb.velocity = new Vector2(contextRb.velocity.y * -1, contextRb.velocity.x);
                    break;

                default: receiverRb.velocity = Vector2.zero;  break;
            }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            if (other.gameObject.CompareTag("LimitROOF"))
                contextRb.velocity = new Vector2(0.0f, -2.0f);
            else if (other.gameObject.CompareTag("LimitGROUND"))
                contextRb.velocity = new Vector2(0.0f, 2.0f);
        }
    }
}
