using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class MoveObject : MonoBehaviour
{
    [SerializeField]
    private NavMeshSurface navSurface;

    private void Start()
    {
        //this.gameObject.transform.parent = null;
        MovingFloor();
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
    private void MovingFloor()
    {
        transform.DOMove(transform.forward * 12, 4)
            .SetRelative(true)
            .SetLoops(-1, LoopType.Yoyo)
            .SetLink(this.gameObject);
    }
}