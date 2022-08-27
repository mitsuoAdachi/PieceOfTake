using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameMode
    {
        Preparate,
        Preparate_Remove,
        Play,
        Stop,
        GameUp
    }
    public GameMode gameMode;

    //ステージデータのリスト
    public List<StageData> stageDatas = new List<StageData>();

    //味方ユニットと敵ユニットのデータのリスト
    public List<UnitData> allyUnitDatas = new List<UnitData>();
    public List<UnitData> enemyUnitDatas = new List<UnitData>();

    //生成された味方ユニットと敵ユニットのリストを用意
    public List<UnitController> AllyList = new List<UnitController>();
    public List<UnitController> EnemyList = new List<UnitController>();

    [SerializeField]
    private UnitGenerator unitGenerator;

    [SerializeField]
    private ModeChange modeChange;

    [SerializeField]
    private UIManager uiManager;

    public int totalCost; //配置ユニットの総コスト

    public int stageLv=1;

    [Header("ユニットの生成待機時間"),SerializeField]
    private float generateIntaervalTime;
    public float GenerateIntaervalTime { get => generateIntaervalTime; }


    void Start()
    {
        //SOデータを読み込む
        SetupSOData();

        //ステージの準備
        PreparateStage(stageLv);

        //ユニット選択ボタンを設定
        uiManager.SetupUnitButton(this, modeChange);

        //各種ボタン押下時の準備
        modeChange.SetupModeChangeButton(this);

        //味方ユニットの生成準備
        StartCoroutine(unitGenerator.LayoutUnit(this,uiManager));

        //敵ユニットの生成
        unitGenerator.PreparateEnemyUnit();

    }

    /// <summary>
    /// ゲーム実行時にunitDataSO内のデータリストをGameManager内のデータリストに収納し直す
    /// </summary>
    public void SetupSOData()
    {
        //ステージデータのリスト
        for (int i = 0; i < DataBaseManager.instance.stageDataSO.stageDataList.Count; i++)
        {
            stageDatas.Add(DataBaseManager.instance.stageDataSO.stageDataList[i]);
        }
        //味方データのリスト
        for (int i = 0; i < DataBaseManager.instance.allyUnitDataSO.allyUnitDatasList.Count; i++)
        {
            allyUnitDatas.Add(DataBaseManager.instance.allyUnitDataSO.allyUnitDatasList[i]);
        }
        ///敵データのリスト
        for (int i = 0; i < DataBaseManager.instance.enemyUnitDataSO.enemyUnitDatasList.Count; i++)
        {
            enemyUnitDatas.Add(DataBaseManager.instance.enemyUnitDataSO.enemyUnitDatasList[i]);
        }
    }

    /// <summary>
    /// ステージ毎に敵ユニットを配置する
    /// </summary>
    /// <param name="index"></param>
    public void PreparateStage(int index)
    {
        //ステージデータが持つ敵の配置情報を敵をunitGeneratorへ送る
        foreach (Vector3 stages in stageDatas[index].enemyTrans)
        {
            unitGenerator.enemyTrans.Add(stages);
        }

        //ステージデータの採番
        //for (int i = 0; i < stageDatas.Count; i++)
        //{
        //    stageDatas[i].stageLvIndex = i;
        //}

        //StageInfo stage = Instantiate(stageDatas[index].stagePrefab);
    }
}
