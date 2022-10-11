using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScytheController : MonoBehaviour
{
    private UnitController unitController;

    // Start is called before the first frame update
    void Start()
    {
        OnPendulm();
    }

    /// <summary>
    /// 一定間隔で大鎌が振り子する
    /// </summary>
    private void OnPendulm()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(transform.DORotate(new Vector3(0, 0, 200), 2)
            .SetRelative(true)
            .SetDelay(2))
            .SetLoops(-1, LoopType.Restart);
        //sequence.Pause();
        //sequence.Play();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Ally") || col.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("接触");
            unitController = col.gameObject.GetComponent<UnitController>();
            unitController.OnDamage(1);
            unitController.OnKnockBack(5);
        }
    }
}
