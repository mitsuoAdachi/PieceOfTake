using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutSideCollider : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out UnitController unit) == true)
        {
            unit.OnDie();

            Destroy(other.gameObject);
        }
    }
}
