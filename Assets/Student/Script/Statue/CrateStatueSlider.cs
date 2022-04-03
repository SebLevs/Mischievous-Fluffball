using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateStatueSlider : MonoBehaviour
{
    [SerializeField] private Transform crateStatue;
    [SerializeField] private Collider2D sliderCollider;

    private void FixedUpdate()
    {
        // May need refactoring
        //      - Conditional is sometime launched when clamp occurs at extremities of slider
        if (crateStatue.position.x != transform.position.x)
        {
            float minColX = sliderCollider.bounds.center.x - sliderCollider.bounds.extents.x;
            float maxColX = sliderCollider.bounds.center.x + sliderCollider.bounds.extents.x;
            

            float clampedX = Mathf.Clamp(transform.position.x, minColX, maxColX);
            //Debug.Log($"minx : {minColX}, max x : {maxColX}, clampedx : {clampedX}");

            transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
            crateStatue.position = new Vector3(clampedX, crateStatue.position.y, crateStatue.position.z);
        }
    }
}
