using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class MoveObject : MonoBehaviour
{
    private void Start()
    {
        MovingFloor();
    }

    private void MovingFloor()
    {
        transform.DOMove(transform.forward * 12, 8)
            .SetRelative(true)
            .SetLoops(-1, LoopType.Yoyo)
            .SetLink(this.gameObject);  
    }
}