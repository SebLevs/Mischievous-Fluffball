using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorStopper : MonoBehaviour
{
    // SECTION - Field --------------------------------------------------------------------
    [SerializeField] private bool isAlsoPathBlocker = false;
    
    [SerializeField] private bool isAlsoBenchSpawnner = false;
    [SerializeField] private GameObject benchPrefab;

    [SerializeField] private GameObject PathBlockerObject;
    [SerializeField] private Transform pathToBlockTr;


    // SECTION - Method --------------------------------------------------------------------
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Contains("Elevator"))
        {
            collision.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

            if (isAlsoPathBlocker)
                Instantiate(PathBlockerObject, pathToBlockTr);

            if (isAlsoBenchSpawnner)
            {
                benchPrefab.SetActive(true);
            }
        }
    }
}
