using UnityEngine;

public class TriggerPooling : MonoBehaviour
{
    [SerializeField] GameObject triggerObject;
    [SerializeField] PoolPattern poolScript;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name.Contains(triggerObject.name))
            poolScript.PoolTriggger = true;
    }
}
