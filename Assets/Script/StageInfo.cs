using UnityEngine;
using System.Collections.Generic;

public class StageInfo : MonoBehaviour
{
    private GameManager gameManager;

    public GenerateUnitData[] generateUnitDatas;

    /// <summary>
    /// UnitGeneratorクラスよりGameManagerコンポーネントを取得
    /// </summary>
    /// <param name="gameManager"></param>
    public void SetupStageInfo(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public List<UnitController> GenerateEnemys()
    {
        List<UnitController> unitList = new List<UnitController>();

        for (int i = 0; i < generateUnitDatas.Length; i++)
        {
            UnitController unit = Instantiate(DataBaseManager.instance.GetUnitController(generateUnitDatas[i].unitId), generateUnitDatas[i].generateTran.position, generateUnitDatas[i].generateTran.rotation);

            unit.StartMoveUnit(gameManager, gameManager.GenerateAllyList);

            unit.SetupUnitStateEnemy(gameManager.enemyUnitDatas);

            unitList.Add(unit);
        }
        return unitList;
    }
}



