using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyUnitDataSO", menuName = "Create EnemyUnitDataSO")]
public class EnemyUnitDataSO : ScriptableObject
{
    public List<UnitData> enemyUnitDatasList = new List<UnitData>();
}