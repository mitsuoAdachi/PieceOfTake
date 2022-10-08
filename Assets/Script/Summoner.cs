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
    private UnitController creature2;
    private UnitController creature3;

    //public void OnSummonCreature()
    //{
    //    StartCoroutine(SummonCreature());
    //}

    //public IEnumerator SummonCreature()
    //{
    //    tran = summoner.transform.localPosition;
    //    tran.x = Mathf.Clamp(tran.x, tran.x - 0.5f, tran.x - 1);
    //    tran.z = Mathf.Clamp(tran.z, tran.z - 0.5f, tran.z - 1);

    //    SetupCreatureController(creature1);
    //    yield return new WaitForSeconds(0.3f);
    //    SetupCreatureController(creature2);
    //    yield return new WaitForSeconds(0.3f);
    //    SetupCreatureController(creature3);
    //}

    public void SummonCreature()
    {
        tran = summoner.transform.localPosition;
        tran.x = Mathf.Clamp(tran.x, tran.x - 0.5f, tran.x - 1);
        tran.z = Mathf.Clamp(tran.z, tran.z - 0.5f, tran.z - 1);

        SetupCreatureController(creature1);
    }

    private void SetupCreatureController(UnitController creature)
    {
        creature = Instantiate(creaturePrefab, new Vector3(tran.x, tran.y, tran.z), Quaternion.identity);

        creature.StartMoveUnit(summoner.gameManager, summoner.gameManager.GenerateAllyList);
        creature.SetupUnitStateEnemy(summoner.gameManager.enemyUnitDatas);
        summoner.gameManager.GenerateEnemyList.Add(creature);
    }
}
