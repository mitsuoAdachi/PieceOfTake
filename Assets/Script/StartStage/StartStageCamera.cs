using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class StartStageCamera : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera cvCam;
   
    private CinemachineTrackedDolly trackDolly;

    // Start is called before the first frame update
    void Start()
    {
        trackDolly = cvCam.GetCinemachineComponent<CinemachineTrackedDolly>();
    }

    // Update is called once per frame
    void Update()
    {
        trackDolly.m_PathPosition += 0.001f;
    }
}
