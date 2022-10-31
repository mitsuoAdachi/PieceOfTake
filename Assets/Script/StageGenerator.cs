using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class StageGenerator : MonoBehaviour
{
    [SerializeField]
    List<GameObject> stageLayerList = new List<GameObject>();

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

        //前回のステージのstageLayerListを消去する
        if(stageLayerList.Count != 0)
           stageLayerList.Clear();

        //配置可能エリア用のレイアーへ切り替えるため、生成したステージをリストに追加する
        for (int i = 0; i < stage.StageObj.Length; i++)
        {
            stageLayerList.Add(stage.StageObj[i]);
        }

        //StageInfoクラスへgameManager要素を渡す
        stage.SetupStageInfo(gameManager);

        //敵をステージに生成する
        gameManager.GenerateEnemyList = stage.GenerateEnemys();

        //Navmeshをベイク
        LoadNavmesh();

        //SkyBoxの設定
        SetupSkyBox(stageLevelIndex);
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
    }

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

    /// <summary>
    /// Preparateモード時とPlayモード時で特定のエリアのレイヤーを切り替える
    /// </summary>
    public void SwitchStageLayer()
    {
        if (gameManager.gameMode == GameManager.GameMode.Preparate)
        {
            for(int i = 0; i < stageLayerList.Count; i++)
            {
                stageLayerList[i].layer = 2;
            }
        }

        if (gameManager.gameMode == GameManager.GameMode.Play)
        {
            for (int i = 0; i < stageLayerList.Count; i++)
            {
                stageLayerList[i].layer = 3;
            }
        }

    }
}