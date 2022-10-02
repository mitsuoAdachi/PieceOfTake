using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBallController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody ballRigid;

    [SerializeField]
    private Rigidbody ballPrefab;

    [SerializeField]
    Transform ballTran;

    private bool isBallAttack;

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (isBallAttack)
        {
            if(ballRigid != null)
            ballRigid.AddForce(transform.forward * 10, ForceMode.Acceleration);
        }
        //Debug.Log("SqrMag"+ballRigid.velocity.sqrMagnitude);
    }

        public void BallAttack()
    {
        ballRigid.isKinematic = false;
        ballRigid.gameObject.transform.parent = null;
        StartCoroutine(OnBallAttack());
    }

    private IEnumerator OnBallAttack()
    {
        isBallAttack = true;

        yield return new WaitForSeconds(2);

        ballRigid = null;

        ballRigid = Instantiate(ballPrefab, ballTran, false);

        isBallAttack = false;
    }
}
