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

    [Header("ユニットの生成待機時間")]
    public float generateIntaervalTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(unitGenerator.LayoutUnit(this));

        for (int i = 0; i < EnemyList.Count; i++)
            StartCoroutine(EnemyList[i].MoveUnit(this,AllyList));
    }
}
