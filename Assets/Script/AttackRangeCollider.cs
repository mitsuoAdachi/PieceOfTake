using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeCollider : MonoBehaviour
{
    [SerializeField]
    private UnitController myUnitController;

    private void OnTriggerStay(Collider other)
    {
        if (myUnitController.TargetUnit == null)
            return;

        if (other.gameObject == myUnitController.TargetUnit.gameObject)
        {
            myUnitController.isAttack = true;

            myUnitController.PreparateAttack();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        myUnitController.isAttack = false;
    }

    private void Reset()
    {
        transform.parent.TryGetComponent(out myUnitController);
    }

}
