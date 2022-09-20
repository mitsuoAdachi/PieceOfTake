using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StageGenerator : MonoBehaviour
{
    private NavMeshDataInstance instance;

    private GameManager gameManager;

    /// <summary>
    /// ステージレベルに合わせてステージデータを生成し、敵ユニットも生成する
    /// </summary>
    /// <param name="stageLevelIndex"></param>
    public void PreparateStage(int stageLevelIndex, GameManager gameManager)
    {
        this.gameManager = gameManager;

        //ステージ生成
        StageInfo stage = Instantiate(gameManager.stageDatas[stageLevelIndex].stagPrefab);

        //Navmeshをベイク
        LoadNavmesh();

        //配置された敵ユニットに移動性能とステータスを付与
        foreach (UnitController enemy in stage.enemyPrefabs)
        {
            enemy.StartMoveUnit(gameManager, gameManager.GenerateAllyList);

            enemy.SetupUnitStateEnemy(gameManager.enemyUnitDatas);

            gameManager.GenerateEnemyList.Add(enemy);
        }
    }

    /// <summary>
    /// ステージ生成時にNavmeshをベイクする
    /// </summary>
    /// <param name="gameManager"></param>
    public void LoadNavmesh()
    {
        // NavMeshの生成
        string assetname = gameManager.stageDatas[gameManager.stageLevel].navmeshData.name;
        NavMeshData navemeshBake = Resources.Load<NavMeshData>(assetname);
        //instance = NavMesh.AddNavMeshData(navemeshBake);
    }

    //public void DeleteNavmesh()
    //{
    //    // NavMeshの破棄
    //    NavMesh.RemoveNavMeshData(instance);
    //}

}