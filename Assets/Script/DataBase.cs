using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBase : MonoBehaviour
{

    //ステージデータのリスト
    public List<StageData> stageDatas = new List<StageData>();

    //味方ユニットと敵ユニットのデータのリスト
    public List<UnitData> allyUnitDatas = new List<UnitData>();
    public List<UnitData> enemyUnitDatas = new List<UnitData>();

    //生成された味方ユニットと敵ユニットのリストを用意
    public List<UnitController> GenerateAllyList = new List<UnitController>();
    public List<UnitController> GenerateEnemyList = new List<UnitController>();

    //ユニットカメラのリスト
    public List<CameraController> unitCamList = new List<CameraController>();
    // Start is called before the first frame update

    public static DataBase instance;

    //シングルトンの作成
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
