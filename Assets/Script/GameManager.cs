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

    public List<UnitController> LayoutUnitList = new List<UnitController>();

    public List<UnitController> EnemyUnitList = new List<UnitController>();

    [SerializeField]
    private UnitController unitController;

    [SerializeField]
    private UnitGenerator unitGenerator;

    [Header("ユニットの生成待機時間")]
    public float generateIntaervalTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(unitGenerator.LayoutUnit(this));

        StartCoroutine(unitController.MoveUnit(this));
    }

}
