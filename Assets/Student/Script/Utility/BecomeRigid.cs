using UnityEngine;

public class BecomeRigid : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("CanBecomeRigid") && !other.gameObject.GetComponent<Rigidbody2D>())

        {
            other.gameObject.AddComponent<Rigidbody2D>();
            other.gameObject.tag = "LightWeightObject";
        }

    }
}
