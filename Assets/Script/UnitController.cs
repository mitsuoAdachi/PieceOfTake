using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    private UnitController targetUnit;

    [SerializeField,Header("ユニットの移動速度")]
    private float moveSpeed=0.5f;

    /// <summary>
    /// ユニットの移動
    /// </summary>
    /// <param name="gameManager"></param>
    /// <returns></returns>
    public IEnumerator MoveUnit(GameManager gameManager)
    {
        //Debug.Log("監視開始");
        while (true)
        {
            if (gameManager.gameMode == GameManager.GameMode.Preparate)
            {
                //EnemyListに登録してあるユニットの内、一番近いユニットに向かって移動する
                float standardDistanceValue = 1000;

                foreach (UnitController target in gameManager.EnemyUnitList)
                {
                    float nearTargetDistanceValue = Vector3.Distance(transform.position, target.transform.position);

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
    private void Start()
    {
        
    }
}
