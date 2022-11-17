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
        Play
    }
    public GameMode gameMode;

    //セットアップメソッド用、各コンポーネントを取得
    [SerializeField]
    public UnitGenerator unitGenerator;
    [SerializeField]
    private ModeChange modeChange;
    [SerializeField]
    private UIManager uiManager;
    [SerializeField]
    private CameraManager camManager;

    public StageGenerator stageGenerator;

    public StageNumberDisplay stageNumberDisplay;

    //生成するステージのレベル
    [SerializeField]
    public static int stageLevel = 8; //8

    //配置ユニットの総コスト
    public int totalCost;

    //各UIのイメージ画像
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

    //勝敗時の音響
    [SerializeField]
    AudioClip winAudio1;
    [SerializeField]
    AudioClip winAudio2;
    [SerializeField]
    AudioClip loseAudio;

    //ボタン押下時の効果音
    [SerializeField]
    private AudioClip pushAudio;

    //次ステージに進む用のボタン
    [SerializeField]
    private Button nextButton;

    //ステージ切り替え時の演出用変数
    [SerializeField]
    private Fade fade;

    //点滅用
    [SerializeField]
    private Material flashingMaterial;

    //DOTween演出繋ぎ用
    private Sequence sequence;


    void Start()
    {
        //SOデータを読み込む
        SetupSOData();

        //フェイドイン演出
        SceneStartFade();

        //ステージの準備
        stageGenerator.PreparateStage(stageLevel, this);

        //ユニット選択ボタンを設定
        uiManager.SetupUnitButton(this, modeChange);

        //各種ボタン押下時の準備
        modeChange.SetupChangeModeButton(this);
        camManager.SetupChangeCam();

        //コスト表示
        unitGenerator.SetupCostRatio(this);

        //味方ユニットの生成準備
        StartCoroutine(unitGenerator.LayoutUnit(uiManager));

        //勝敗条件の監視
        StartCoroutine(JudgeStageClear());

        //配置可能エリアを点滅開始
        StageFlashing();
        sequence.Restart();

    }

    /// <summary>
    /// ゲーム実行時にunitDataSO内のデータリストをGameManager内のデータリストに格納する
    /// </summary>
    public void SetupSOData()
    {
        //ステージデータのリスト
        for (int i = 0; i < DataBaseManager.instance.stageDataSO.stageDataList.Count; i++)
        {
            DataBase.instance.stageDatas.Add(DataBaseManager.instance.stageDataSO.stageDataList[i]);
        }
        //味方データのリスト
        for (int i = 0; i < DataBaseManager.instance.allyUnitDataSO.allyUnitDatasList.Count; i++)
        {
            DataBase.instance.allyUnitDatas.Add(DataBaseManager.instance.allyUnitDataSO.allyUnitDatasList[i]);
        }
        ///敵データのリスト
        for (int i = 0; i < DataBaseManager.instance.enemyUnitDataSO.enemyUnitDatasList.Count; i++)
        {
            DataBase.instance.enemyUnitDatas.Add(DataBaseManager.instance.enemyUnitDataSO.enemyUnitDatasList[i]);
        }
    }

    /// <summary>
    /// 勝敗の条件を設定
    /// </summary>
    /// <returns></returns>
    private IEnumerator JudgeStageClear()
    {
        //勝敗条件用の分岐
        bool isJudgeClear = true;

        while (isJudgeClear == true)
        {
            Debug.Log("監視開始");

            if (gameMode == GameMode.Play && DataBase.instance.GenerateEnemyList.Count <= 0)
            {
                Debug.Log("勝利");

                AudioSource.PlayClipAtPoint(winAudio1, Camera.main.transform.position, 1f);

                yield return new WaitForSeconds(1);

                AudioSource.PlayClipAtPoint(winAudio2, Camera.main.transform.position, 0.2f);

                uiManager.OnDOTweenUI(stageClearImage);
                DOVirtual.DelayedCall(4, () =>
                {
                    uiManager.OnDOTweenUI(titleImage);
                    uiManager.OnDOTweenUI(nextImage);
                });

                isJudgeClear = false;

            }

            if (gameMode == GameMode.Play && DataBase.instance.GenerateAllyList.Count <= 0)
            {
                Debug.Log("敗北");

                AudioSource.PlayClipAtPoint(loseAudio, Camera.main.transform.position, 1);

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

    public void JudgeTotalCost()
    {
        //totalCostがステージコストと同じ値なら
        if (DataBase.instance.stageDatas[stageLevel].stageCost <= totalCost)
        {
            Debug.Log("これ以上はユニットを設置できません");

            //Alpha値をリセットする
            flashingMaterial.color = new Color32(214, 207, 207, 255);

            //配置可能エリアの点滅中断
            sequence.Pause();
        }
        else
        {
            //配置可能エリアの再点滅
            Debug.Log("ユニットを設置できます");
            sequence.Restart();
        }
    }

    /// <summary>
    /// 配置可能エリアの点滅
    /// </summary>
    private void StageFlashing()
    {
        flashingMaterial.color = new Color32(214, 207, 207, 255);

        sequence = DOTween.Sequence()
            .Append(flashingMaterial.DOFade(0.85f, 0.2f)
            .SetDelay(0.1f))
            .SetLoops(-1, LoopType.Yoyo)
            .Pause()
            .SetAutoKill(false)
            .SetLink(gameObject);
    }
}