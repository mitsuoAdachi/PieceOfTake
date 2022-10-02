using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordManAttackCollider : MonoBehaviour
{
    [SerializeField]
    private BoxCollider col;

    private UnitController enemy;

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("敵発見");

        if (other.CompareTag("Enemy"))
        {
            Debug.Log("敵発見");

            enemy = other.GetComponent<UnitController>();

            StartCoroutine(OnAttack());
        }
    }
    private IEnumerator OnAttack()
    {
        enemy.OnDamage(1);
        enemy.OnKnockBack(1);
        col.enabled = false;

        yield return new WaitForSeconds(1);

        col.enabled = true;
    }
}
