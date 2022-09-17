using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OutSideDropCollider : MonoBehaviour
{

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ally") || other.CompareTag("Enemy"))
        {
            other.GetComponent<UnitController>().isGround = false;
        }
    }
}
