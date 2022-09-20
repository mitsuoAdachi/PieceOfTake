using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera cvCam;

    private void Update()
    {
        ScalingCamera();
    }

    public void ScalingCamera()
    {
        if (Input.GetButton("z"))
            cvCam.m_Lens.FieldOfView += 0.15f;

        if (Input.GetButton("x"))
            cvCam.m_Lens.FieldOfView -= 0.15f;

    }
}
