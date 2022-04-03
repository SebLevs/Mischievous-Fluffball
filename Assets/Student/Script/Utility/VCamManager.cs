using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VCamManager : MonoBehaviour
{
    // SECTION - Field --------------------------------------------------------------------
    [Header("Virtual Camera Reference")]
    [SerializeField] private CinemachineVirtualCamera vCam;

    [Space(10)][Header("Orthographic Size")]
    [SerializeField] private float minLensOrtho;
    [SerializeField] private float maxLensOrtho = 8.0f;

    [Space(10)][Header("Screen Y")]
    [SerializeField] private float minM_ScreenY;
    [SerializeField] private float maxM_ScreenY = 0.75f;
                     private CinemachineFramingTransposer vCamBody;


    // SECTION - Method --------------------------------------------------------------------
    private void Start()
    {
        // Set base values for back to normal OnTriggerExit()
        minLensOrtho = vCam.m_Lens.OrthographicSize;
        vCamBody = vCam.GetCinemachineComponent<CinemachineFramingTransposer>();
        minM_ScreenY = vCamBody.m_ScreenY;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            vCam.m_Lens.OrthographicSize = maxLensOrtho;
            vCamBody.m_ScreenY = maxM_ScreenY;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            vCam.m_Lens.OrthographicSize = minLensOrtho;
            vCamBody.m_ScreenY = minM_ScreenY;
        }
    }
}
