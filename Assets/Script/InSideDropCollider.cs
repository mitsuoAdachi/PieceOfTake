using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InSideDropCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ally") || other.CompareTag("Enemy"))
        {
            Debug.Log("isGround");
            other.GetComponent<UnitController>().isGround = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ally") || other.CompareTag("Enemy"))
        {
            Debug.Log("isAir");
            other.GetComponent<UnitController>().isGround = false;
        }
    }
}
