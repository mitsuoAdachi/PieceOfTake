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

    public List<UnitController> EnemyList = new List<UnitController>();
    public List<UnitController> AllyList = new List<UnitController>();

    [SerializeField]
    private UnitGenerator unitGenerator;

    [SerializeField]
    private ModeChange modeChange;

    [Header("ユニットの生成待機時間")]
    public float generateIntaervalTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(unitGenerator.LayoutUnit(this));

        for (int i = 0; i < EnemyList.Count; i++)
            StartCoroutine(EnemyList[i].MoveUnit(this,AllyList));

        GameModeChangeButon();

    }

    private void GameModeChangeButon()
    {
        modeChange.btnModeChange.onClick.AddListener(()=>modeChange.GameModeChange(this));
    }
}
