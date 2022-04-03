using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportObjects : MonoBehaviour
{
    [SerializeField] GameObject tpObject;
    [SerializeField] Transform tpPosition;
    [SerializeField] bool shouldSpawnAtPlayer = false;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject == tpObject)
        {
            // Spawn at player to avoid confusion
            if (shouldSpawnAtPlayer)
            {
                GameObject player = GameObject.Find("Player");
                float pX = player.transform.position.x;
                float pY = player.transform.position.y + 1.0f;

                other.transform.position = new Vector3(pX, pY, other.transform.position.z);
            }
            // Spawn at manually set position
            else
                other.transform.position = tpPosition.position;
        }
    }
}
