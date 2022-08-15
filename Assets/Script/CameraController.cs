using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    GameObject rotAxis;

    [SerializeField]
    float rotSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            float angle = Input.GetAxis("Horizontal") * rotSpeed;

            Vector3 axisPos = rotAxis.transform.position;

            transform.RotateAround(axisPos, Vector3.up, angle);
        
    }
}
