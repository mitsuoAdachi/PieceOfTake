using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField]
    GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        MovingFloor();
    }

    void MovingFloor()
    {
        //transform.DOLocalMove(transform.forward * 5, 5)
        //   .SetRelative(true)
        //   .SetLoops(-1, LoopType.Yoyo)
        //   .SetLink(this.gameObject);
        transform.DOLocalMove(obj.transform.position, 5)
               .SetRelative(true)
               .SetLoops(-1, LoopType.Yoyo)
               .SetLink(this.gameObject);

    }
}
