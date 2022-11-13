using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour
{
    private int damage = 2;
    private float knockBackPower = 1.0f;

    private UnitController enemy;

    [SerializeField]
    private Rigidbody ball;

    private float dynamicBallValue = 0.5f;

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("接触１");
            //Debug.Log(ball.velocity.sqrMagnitude);
            if (ball.velocity.sqrMagnitude > dynamicBallValue)
            {
                Debug.Log("接触2");

                enemy = col.gameObject.GetComponent<UnitController>();
                enemy.OnDamage(damage);
                enemy.OnKnockBack(knockBackPower);
            }
        }
    }
}
