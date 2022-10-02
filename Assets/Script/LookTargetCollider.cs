using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookTargetCollider : MonoBehaviour
{
    [SerializeField]
    private UnitController unitController;

    private void OnTriggerStay(Collider other)
    {
        if(unitController.TargetUnit != null)
        //進行方向を向く
        unitController.transform.LookAt(unitController.TargetUnit.transform.position);
    }

    private void Reset()
    {
        transform.parent.TryGetComponent(out unitController);
    }

}
