using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeCollider : MonoBehaviour
{
    [SerializeField]
    private UnitController unitController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == unitController.TargetUnit.gameObject)
        {
            StartCoroutine(unitController.PreparateAttack());
        }

        //if (other.gameObject.TryGetComponent(out targetUnitController))
        //{
        //   StartCoroutine(unitController.PreparateAttack());
        //}
    }
}
