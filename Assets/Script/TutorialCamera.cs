using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCamera : MonoBehaviour
{
    [SerializeField]
    private Camera Cam;

    [SerializeField]
    private GameObject camAxis;

    private void Start()
    {
        StartCoroutine(ScalingCamera());

    }

    private IEnumerator ScalingCamera()
    {
        while (true)
        {
            this.transform.LookAt(camAxis.transform);

            Cam.transform.RotateAround(camAxis.gameObject.transform.position, Vector3.up, 0.05f);

            yield return null;
        }       
    }
}
