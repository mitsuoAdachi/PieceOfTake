using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AllyUnitDataSO", menuName = "Create AllyUnitDataSO")]
public class AllyUnitDataSO : ScriptableObject
{
    public List<UnitData> allyUnitDatasList = new List<UnitData>();
}