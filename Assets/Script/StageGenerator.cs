using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    /// <summary>
    /// ステージレベルに合わせてステージデータを生成し、敵ユニットも生成する
    /// </summary>
    /// <param name="stageLevelIndex"></param>
    public void PreparateStage(int stageLevelIndex, GameManager gameManager)
    {
        StageInfo stage = Instantiate(gameManager.stageDatas[stageLevelIndex].stagPrefab);

        foreach (UnitController enemy in stage.enemyPrefabs)
        {
            enemy.StartMoveUnit(gameManager, gameManager.GenerateAllyList);

            enemy.SetupUnitStateEnemy(gameManager.enemyUnitDatas);

            gameManager.GenerateEnemyList.Add(enemy);
        }
    }
}