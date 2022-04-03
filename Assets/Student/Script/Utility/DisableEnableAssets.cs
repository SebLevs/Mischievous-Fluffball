using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableEnableAssets : MonoBehaviour
{
    // SECTION - field --------------------------------------------------------------------
    [SerializeField] private List<GameObject> objectList;
    [SerializeField] private bool activeOrinactive = false;


    // SECTION - Method --------------------------------------------------------------------
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            ManageSetEnable(activeOrinactive);
    }

    private void ManageSetEnable(bool toggleTo)
    {
        foreach (GameObject obj in objectList)
            obj.SetActive(toggleTo);
    }
}
