using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera cvCam;

    private float zoomValue = 0.15f;

    private void LateUpdate()
    {
        ScalingCamera();
    }

    public void ScalingCamera()
    {
        //カメラのズームアップ、ズームアウトの上限を設定
        cvCam.m_Lens.FieldOfView = Mathf.Clamp(cvCam.m_Lens.FieldOfView, 20, 100);

        if (Input.GetButton("z"))
            cvCam.m_Lens.FieldOfView -= zoomValue;

        if (Input.GetButton("x"))
            cvCam.m_Lens.FieldOfView += zoomValue;
    }
}
