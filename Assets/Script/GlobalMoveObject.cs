using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GlobalMoveObject : MonoBehaviour
{
    [SerializeField]
    private GameObject[] moveObj;

    private void Start()
    {
        MovingFloor();
    }

    private void MovingFloor()
    {
        for (int i = 0; i < moveObj.Length; i++)
        {
            //    transform.DOMove(moveObj[i].transform.forward * 12, 8)
            //        .SetRelative(true)
            //        .SetLoops(-1, LoopType.Yoyo)
            //        .SetLink(this.gameObject);
            //}
            //moveObj[0].transform.DOLocalMove(Vector3.forward*5, 5)
            //    //.SetRelative(true)
            //    .SetLoops(-1, LoopType.Yoyo)
            //    .SetLink(this.gameObject);

            //moveObj[1].transform.DOLocalMove(Vector3.forward*5, 5)
            //    .SetRelative(true)
            //    .SetLoops(-1, LoopType.Yoyo)
            //    .SetLink(this.gameObject);

            //transform.DOLocalMove(moveObj[2].transform.forward*5, 5)
            //    //.SetRelative(true)
            //    .SetLoops(-1, LoopType.Yoyo)
            //    .SetLink(this.gameObject);

            transform.DOLocalMove(moveObj[i].transform.forward * 5, 5)
            .SetRelative(true)
            .SetLoops(-1, LoopType.Yoyo)
            .SetLink(this.gameObject);

            //moveObj[4].transform.DOLocalMoveZ(12, 5)
            //    //.SetRelative(true)
            //    .SetLoops(-1, LoopType.Yoyo)
            //    .SetLink(this.gameObject);

            //moveObj[5].transform.DOLocalMoveZ(12, 5)
            //    .SetRelative(true)
            //    .SetLoops(-1, LoopType.Yoyo)
            //    .SetLink(this.gameObject);

            //moveObj[5].transform.DOLocalMoveZ(12, 5)
            //    //.SetRelative(true)
            //    .SetLoops(-1, LoopType.Yoyo)
            //    .SetLink(this.gameObject);

        }

    }
}