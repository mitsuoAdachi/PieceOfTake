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

    //味方ユニットと敵ユニットのリストを用意
    public List<UnitController> AllyList = new List<UnitController>();
    public List<UnitController> EnemyList = new List<UnitController>();

    [SerializeField]
    private UnitGenerator unitGenerator;

    [SerializeField]
    private ModeChange modeChange;

    [SerializeField]
    private UIManager uiManager;

    [Header("ユニットの生成待機時間"),SerializeField]
    private float generateIntaervalTime;
    public float GenerateIntaervalTime { get => generateIntaervalTime; }

    void Start()
    {
        //味方ユニットの生成準備
        StartCoroutine(unitGenerator.LayoutUnit(this));

        //敵ユニットの移動準備
        for (int i = 0; i < EnemyList.Count; i++)
            StartCoroutine(EnemyList[i].MoveUnit(this,AllyList));

        //各種ボタン押下時の準備
        SetupButton();

        //ユニットデータの準備
        unitGenerator.SetupUnitData();     
    }

    /// <summary>
    /// 各種ボタン押下時の設定
    /// </summary>
    private void SetupButton()
    {
        //モード変更ボタンの設定
        modeChange.BtnModeChange.onClick.AddListener(() => modeChange.GameModeChange(this));

        //ユニット選択ボタンの設定
        for (int i = 0; i < uiManager.unitSelectButtons.Length; i++)
        {
            uiManager.unitSelectButtons[i].onClick.AddListener(() => uiManager.UnitSelect());
        }
    }
}
