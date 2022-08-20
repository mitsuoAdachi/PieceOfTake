using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeCollider : MonoBehaviour
{
    [SerializeField]
    private UnitController unitController;

    private void OnTriggerEnter(Collider other)
    {
        if (!unitController.isAttack)
        {
            if (other.gameObject == unitController.TargetUnit.gameObject)
            {
                unitController.isAttack = true;

                StartCoroutine(unitController.PreparateAttack());
            }
        }
    }
}
