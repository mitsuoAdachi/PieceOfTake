using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class BakeNavmesh : MonoBehaviour
{
    [SerializeField]
    private NavMeshSurface navSurface;

    //[SerializeField]
    //private GameObject[] moveFloor;

    private void Start()
    {
        //MovingFloor();
    }

    // Update is called once per frame
    void Update()
    {
        RunTimeNavmeshBake();
    }

    /// <summary>
    /// Navmeshをランタイムでベイクし続ける処理
    /// </summary>
    private void RunTimeNavmeshBake()
    {
        navSurface.BuildNavMesh();
    }

    /// <summary>
    /// 移動床
    /// </summary>
    //private void MovingFloor()
    //{
    //    for (int i = 0; i < moveFloor.Length; ++i)
    //    {
    //        moveFloor[i].transform.parent = null;

    //        //moveFloor[i].transform.DOMove(Vector3.forward * 12, 6)
    //        moveFloor[i].transform.DOLocalMove(transform.forward*12, 6)
    //            //.SetRelative(true)
    //            .SetLoops(-1, LoopType.Yoyo)
    //            .SetLink(this.gameObject);
    //    }
    //}
}