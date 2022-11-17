using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Summoner : MonoBehaviour
{
    [SerializeField]
    private UnitController creaturePrefab;

    [SerializeField]
    private Transform creatureTran;

    [SerializeField]
    private UnitController summoner;

    private Vector3 tran;

    private UnitController creature1;

    public void OnSummonCreature()
    {
        StartCoroutine(SummonCreature());
    }

    public IEnumerator SummonCreature()
    {
        tran = summoner.transform.localPosition;
        tran.x = Mathf.Clamp(tran.x, tran.x - 0.5f, tran.x - 1);
        tran.z = Mathf.Clamp(tran.z, tran.z - 0.5f, tran.z - 1);

        SetupCreatureController(creature1);
        yield return new WaitForSeconds(0.3f);
        SetupCreatureController(creature1);
        yield return new WaitForSeconds(0.3f);
        SetupCreatureController(creature1);
    }

    private void SetupCreatureController(UnitController creature)
    {
        creature = Instantiate(creaturePrefab, new Vector3(tran.x, tran.y, tran.z), Quaternion.identity);

        creature.SetupUnitStateEnemy(DataBase.instance.enemyUnitDatas);
        creature.StartMoveUnit(summoner.gameManager, DataBase.instance.GenerateAllyList);
        DataBase.instance.GenerateEnemyList.Add(creature);
    }
}
