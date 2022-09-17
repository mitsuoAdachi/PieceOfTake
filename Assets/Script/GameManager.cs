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

    public int unitsIndex;

    //ステージデータのリスト
    public List<StageData> stageDatas = new List<StageData>();

    //味方ユニットと敵ユニットのデータのリスト
    public List<UnitData> allyUnitDatas = new List<UnitData>();
    public List<UnitData> enemyUnitDatas = new List<UnitData>();

    //生成された味方ユニットと敵ユニットのリストを用意
    public List<UnitController> GenerateAllyList = new List<UnitController>();
    public List<UnitController> GenerateEnemyList = new List<UnitController>();

    [SerializeField]
    private UnitGenerator unitGenerator;

    [SerializeField]
    private ModeChange modeChange;

    [SerializeField]
    private UIManager uiManager;

    public int totalCost; //配置ユニットの総コスト

    public int stageLevel=0;

    [Header("ユニットの生成待機時間"),SerializeField]
    private float generateIntaervalTime;
    public float GenerateIntaervalTime { get => generateIntaervalTime; }

    [SerializeField]
    private Transform stageTran;


    void Start()
    {
        //SOデータを読み込む
        SetupSOData();

        //ステージの準備
        PreparateStage(stageLevel);

        //ユニット選択ボタンを設定
        uiManager.SetupUnitButton(this, modeChange);

        //各種ボタン押下時の準備
        modeChange.SetupChangeModeButton(this);

        //味方ユニットの生成準備
        StartCoroutine(unitGenerator.LayoutUnit(this,uiManager));

        
    }

    /// <summary>
    /// ゲーム実行時にunitDataSO内のデータリストをGameManager内のデータリストに格納する
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
    /// ステージレベルに合わせてステージデータを生成し、敵ユニットも生成する
    /// </summary>
    /// <param name="stageLevelIndex"></param>
    public void PreparateStage(int stageLevelIndex)
    {
        //StageInfo stage = Instantiate(stageDatas[stageLevelIndex].stagPrefab,stageTran,false);
        StageInfo stage = Instantiate(stageDatas[stageLevelIndex].stagPrefab);

        //for(int i=0;i<stage.enemyPrefabs.Length;i++ )
        foreach (UnitController enemy in stage.enemyPrefabs)
        {
            //units.GetComponent<UnitController>();

            enemy.StartMoveUnit(this, GenerateAllyList);

            enemy.SetupUnitStateEnemy(enemyUnitDatas);

            GenerateEnemyList.Add(enemy);
        }
    }
}
