using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class StageGenerator : MonoBehaviour
{

    private GameManager gameManager;

    /// <summary>
    /// ステージレベルに合わせてステージデータを生成し、敵ユニットも生成する
    /// </summary>
    /// <param name="stageLevelIndex"></param>
    public void PreparateStage(int stageLevelIndex, GameManager gameManager)
    {
        this.gameManager = gameManager;

        //SkyBoxの設定
        SetupSkyBox(stageLevelIndex);

        //ステージ生成
        StageInfo stage = Instantiate(gameManager.stageDatas[stageLevelIndex].stagPrefab);

        stage.SetupStageInfo(gameManager);

        //敵をステージに生成する
        gameManager.GenerateEnemyList = stage.GenerateEnemys();

        //Navmeshをベイク
        LoadNavmesh();

    }

    /// <summary>
    /// ステージ生成時にNavmeshをベイクする
    /// </summary>
    /// <param name="gameManager"></param>
    public void LoadNavmesh()
    {
        // NavMeshの生成
        string assetname = gameManager.stageDatas[GameManager.stageLevel].navmeshData.name;
        NavMeshData navemeshBake = Resources.Load<NavMeshData>(assetname);
        //instance = NavMesh.AddNavMeshData(navemeshBake);
    }

    //public void DeleteNavmesh()
    //{
    //    // NavMeshの破棄
    //    NavMesh.RemoveNavMeshData(instance);
    //}

    /// <summary>
    /// 各ステージ毎にSkyBoxを設定する
    /// </summary>
    /// <param name="stageLevelIndex"></param>
    private void SetupSkyBox(int stageLevelIndex)
    {
        //インスタンス化されたステージのstageDatasからSkyBox用のマテリアルデータを取得する
        RenderSettings.skybox = gameManager.stageDatas[stageLevelIndex].sky;

        //SkyBoxマテリアルを回転させる
        gameManager.stageDatas[stageLevelIndex].sky.DOFloat(360, "_Rotation", 360)
            .SetLoops(-1, LoopType.Restart);
    }
}