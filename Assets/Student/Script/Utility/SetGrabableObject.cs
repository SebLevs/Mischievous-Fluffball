using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGrabableObject : MonoBehaviour
{
    [SerializeField] private PlayerContext context;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("LightWeightObject"))
            context.GrabableObject = other.gameObject;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (context.GrabableObject != null)
            if (other.CompareTag("LightWeightObject") && context.GrabableObject.name == other.name)
                context.GrabableObject = null;
    }
}
