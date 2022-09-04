using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    [SerializeField]
    private UnitController myUnitController;

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.TryGetComponent(out UnitController targetUnit))
            {
                targetUnit.Damage(myUnitController.attackPower);
            }
        }

    }
}
