using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class StageData
{
    public int stageLvIndex;
    public int stageCost;
    public StageInfo stagPrefab;
    public NavMeshData navmeshData;
    public Material sky;
}

[System.Serializable]
public class GenerateUnitData
{
    public Transform generateTran;
    public int unitId;
}
