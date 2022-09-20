using UnityEngine;
using UnityEngine.AI;

public class BakeNavmesh : MonoBehaviour
{
    [SerializeField] string assetname = "GameObject";

    private NavMeshDataInstance instance;

    public void LoadNavmesh()
    {
        // NavMeshの登録
        var data = Resources.Load<NavMeshData>(assetname);
        instance = NavMesh.AddNavMeshData(data);
    }

    public void DeleteNavmesh()
    {
        // NavMeshの破棄
        NavMesh.RemoveNavMeshData(instance);
    }
}