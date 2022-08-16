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

    public List<AllyController> AllyUnitList = new List<AllyController>();

    public List<EnemyController> EnemyUnitList = new List<EnemyController>();

    [SerializeField]
    private UnitGenerator unitGenerator;

    [Header("ユニットの生成待機時間")]
    public float generateIntaervalTime;

    // Start is called before the first frame update
    void Start()
    {
            StartCoroutine(unitGenerator.LayoutUnit(this));

        for (int i = 0; i < EnemyUnitList.Count; i++)
            StartCoroutine(EnemyUnitList[i].MoveEnemy(this));

        //for (int i = 0; i < AllyUnitList.Count; i++)
        //    StartCoroutine(AllyUnitList[i].MoveAlly(this));
    }
    private void Update()
    {
        for (int i = 0; i < AllyUnitList.Count; i++)
            AllyUnitList[i].Move(this);
    }

}
