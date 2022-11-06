using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ParticleCollision : MonoBehaviour
{
    private UnitController targetUnit;

    [SerializeField]
    private UnitController myUnitController;

    [SerializeField]
    private ParticleSystem ps;

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Ally"))
        {
            if (other.TryGetComponent(out UnitController targetUnit))
            {
                this.targetUnit = targetUnit;
                targetUnit.OnDamage(myUnitController.attackPower);
                targetUnit.OnKnockBack(myUnitController.blowPower);
            }
        }
    }
    private void OnEnable()
    {
        if(targetUnit !=null)
        ps.transform.DOMove(targetUnit.transform.position, 1);
    }
}
