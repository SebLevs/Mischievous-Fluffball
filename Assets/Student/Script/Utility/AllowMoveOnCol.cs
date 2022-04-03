using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllowMoveOnCol : MonoBehaviour
{
    // SECTION - Field --------------------------------------------------------------------
    [Header("Allowed body to move this Rigidbody2D")]
    [SerializeField] private GameObject allowedObject;
    [SerializeField] private Rigidbody2D contextRb;


    // SECTION - Method --------------------------------------------------------------------
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains(allowedObject.name))
            contextRb.bodyType = RigidbodyType2D.Dynamic;
    }
}
