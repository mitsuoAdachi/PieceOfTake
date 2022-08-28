using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeCollider : MonoBehaviour
{
    //public GameObject colObject; //確認用

    [SerializeField]
    private UnitController unitController;

    private void OnTriggerStay(Collider other)
    {
        if (unitController.TargetUnit != null)
        {
            if (other.gameObject == unitController.TargetUnit.gameObject)
            {
                //colObject = other.gameObject;

                unitController.isAttack = true;

                StartCoroutine(unitController.PreparateAttack());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        unitController.isAttack = false;
    }
}
