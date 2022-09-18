using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookTargetCollider : MonoBehaviour
{
    [SerializeField]
    private UnitController unitController;

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Ally") || other.CompareTag("Enemy"))
        {
            if(unitController.TargetUnit != null)
            //進行方向を向く
            transform.LookAt(unitController.TargetUnit.transform);
        }
    }
}
