using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyController : MonoBehaviour
{
    private EnemyController targetUnit;

    [SerializeField,Header("ユニットの移動速度")]
    private float moveSpeed=0.5f;

    /// <summary>
    /// ユニットの移動
    /// </summary>
    /// <param name="gameManager"></param>
    /// <returns></returns>
    public IEnumerator MoveAlly(GameManager gameManager)
    {
        //Debug.Log("監視開始");
        while (true)
        {
            if (gameManager.gameMode == GameManager.GameMode.Preparate)
            {
                //EnemyListに登録してあるユニットの内、一番近いユニットに向かって移動する

                //敵の距離を比較するための基準となる変数。適当な数値を代入
                float standardDistanceValue = 1000;

                foreach (EnemyController target in gameManager.EnemyUnitList)
                {
                    //EnemyUnitList内に登録してあるオブジェクトとの距離を測り変数に代入する
                    float nearTargetDistanceValue = Vector3.Distance(transform.position, target.transform.position);

                    //基準値より小さければその数値を基準値に代入していき一番小さい数値が変数に残る。その数値を持つオブジェクトが一番近い敵となる
                    if (standardDistanceValue > nearTargetDistanceValue)
                    {
                        standardDistanceValue = nearTargetDistanceValue;

                        //Debug.Log(standardDistanceValue);
                        //if(targetUnit==null)
                        targetUnit = target;
                    }
                }
                //Debug.Log("移動準備完了");
                transform.position = Vector3.MoveTowards(transform.position, targetUnit.transform.position, moveSpeed);
              
            }
            yield return null;
        }
    }

    public void Move(GameManager gameManager)
    {
        Debug.Log("監視開始");
            if (gameManager.gameMode == GameManager.GameMode.Preparate)
            {
                //EnemyListに登録してあるユニットの内、一番近いユニットに向かって移動する

                //敵の距離を比較するための基準となる変数。適当な数値を代入
                float standardDistanceValue = 1000;

                foreach (EnemyController target in gameManager.EnemyUnitList)
                {
                    //EnemyUnitList内に登録してあるオブジェクトとの距離を測り変数に代入する
                    float nearTargetDistanceValue = Vector3.Distance(transform.position, target.transform.position);

                    //基準値より小さければその数値を基準値に代入していき一番小さい数値が変数に残る。その数値を持つオブジェクトが一番近い敵となる
                    if (standardDistanceValue > nearTargetDistanceValue)
                    {
                        standardDistanceValue = nearTargetDistanceValue;

                        //Debug.Log(standardDistanceValue);
                        //if(targetUnit==null)
                        targetUnit = target;
                    }
                }
                Debug.Log("移動準備完了");
                transform.position = Vector3.MoveTowards(transform.position, targetUnit.transform.position, moveSpeed);
            }
    }

    private bool GenerateRange()
    {
        float x = 0;
        float z = 0;
        Mathf.Clamp(x, 100, -100);
        Mathf.Clamp(z, 100, -100);
        if (Input.mousePosition == new Vector3(x, 0, z))
        {
            return true;
        }
        else return false;
    }
}
