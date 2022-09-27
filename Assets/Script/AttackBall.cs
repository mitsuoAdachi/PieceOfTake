using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AttackBall : MonoBehaviour
{
    [SerializeField]
    private Rigidbody ballRigid;

    // Update is called once per frame
    void Update()
    {
        //TODO ボールのインスタンス化
    }

    public void BallAttack()
    {
        ballRigid.isKinematic = false;
        ballRigid.DOMove(transform.forward, 1)
            .SetEase(Ease.InBounce);
    }
}
