using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameMode
    {
        Preparate,
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

    [SerializeField]
    StageLevelIndex stagePrefab;

    private StageLevelIndex stage;

    [Header("ユニットの生成待機時間"),SerializeField]
    private float generateIntaervalTime;
    public float GenerateIntaervalTime { get => generateIntaervalTime; }

    void Start()
    {
        stage = Instantiate(stagePrefab);

        foreach(Transform stages in stage.prefabTran)
        {
            unitGenerator.enemyTrans.Add(stages);
        }
        //for (int i = 0; i < unitGenerator.enemyTrans.Count; i++)
        //{
        //    unitGenerator.enemyTrans.Add(stage.prefabTran[i]);
        //}

            //ユニットデータの準備
            SetupUnitData();

        //味方ユニットの生成準備
        StartCoroutine(unitGenerator.LayoutUnit(this,uiManager));

        //敵ユニットの生成
        unitGenerator.PreparateEnemyUnit();

        ////敵ユニットの移動準備
        //for (int i = 0; i < EnemyList.Count; i++)
        //    EnemyList[i].StartMoveUnit(this,AllyList);

        //ユニット選択ボタンを設定
        uiManager.SetupUnitButton(this);

        //各種ボタン押下時の準備
        modeChange.SetupModeChangeButton(this);
    }

    /// <summary>
    /// ゲーム実行時にunitDataSO内のデータリストをGameManager内のデータリストに収納し直す
    /// </summary>
    public void SetupUnitData()
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
}
