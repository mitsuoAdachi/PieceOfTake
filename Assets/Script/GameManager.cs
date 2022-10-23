using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
    public List<UnitController> GenerateAllyList = new List<UnitController>();
    public List<UnitController> GenerateEnemyList = new List<UnitController>();

    //各コンポーネントを取得
    [SerializeField]
    private UnitGenerator unitGenerator;
    [SerializeField]
    private ModeChange modeChange;
    [SerializeField]
    private UIManager uiManager;

    [SerializeField]
    private LoadScene loadScene;

    public StageGenerator stageGenerator;

    public StageNumberDisplay stageNumberDisplay;

    //生成するステージのレベル
    [SerializeField]
    public static int stageLevel = 10;

    //配置ユニットの総コスト
    public int totalCost; 

    [SerializeField]
    private Image stageClearImage;
    [SerializeField]
    private Image gameOverImage;
    [SerializeField]
    private Image titleImage;
    [SerializeField]
    private Image nextImage;
    [SerializeField]
    private Image againImage;

    private bool isJudgeClear = true;

    [SerializeField]
    private Button nextButton;

    [SerializeField]
    private AudioClip pushAudio;

    [SerializeField]
    private Fade fade;

    void Start()
    {
        //SOデータを読み込む
        SetupSOData();

        //フェイドイン演出
        SceneStartFade();
        loadScene.SetUpLoadScene(this);

        //ステージの準備
        stageGenerator.PreparateStage(stageLevel,this);

        //ユニット選択ボタンを設定
        uiManager.SetupUnitButton(this, modeChange);

        //各種ボタン押下時の準備
        modeChange.SetupChangeModeButton(this);

        //味方ユニットの生成準備
        StartCoroutine(unitGenerator.LayoutUnit(this,uiManager));

        //勝敗条件の監視
        StartCoroutine(JudgeStageClear());

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
    /// 勝敗の条件を設定
    /// </summary>
    /// <returns></returns>
    private IEnumerator JudgeStageClear()
    {
        while (isJudgeClear == true)
        {
            Debug.Log("監視開始");

            if (gameMode == GameMode.Play && GenerateEnemyList.Count <= 0)
            {
                Debug.Log("勝利");

                uiManager.OnDOTweenUI(stageClearImage);
                DOVirtual.DelayedCall(4, () =>
                {
                    uiManager.OnDOTweenUI(titleImage);
                    uiManager.OnDOTweenUI(nextImage);
                });

                isJudgeClear = false;
                
            }

            if(gameMode == GameMode.Play && GenerateAllyList.Count <= 0)
            {
                Debug.Log("敗北");

                uiManager.OnDOTweenUI(gameOverImage);
                DOVirtual.DelayedCall(4, () =>
                {
                    uiManager.OnDOTweenUI(titleImage);
                    uiManager.OnDOTweenUI(againImage);
                });

                isJudgeClear = false;
            }

            yield return null;
        }
    }

    /// <summary>
    /// スタートボタン押下時フェードアウト演出
    /// </summary>
    private void SceneStartFade()
    {
        fade.FadeIn(0.00001f, () =>
        {
            fade.FadeOut(4);
        });
    }
}
