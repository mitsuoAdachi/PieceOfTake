using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropUnitCollider : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ally") || other.CompareTag("Enemy"))
        {
            other.GetComponent<UnitController>().isGround = false;
        }
    }
}
