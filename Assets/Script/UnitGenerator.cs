using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitGenerator : MonoBehaviour
{
    public AllyController unitPrefab;

    private GameManager gameManager;

    private int timer=100;

    /// <summary>
    /// クリックした位置にユニットを生成
    /// </summary>
    /// <returns></returns>
    public IEnumerator LayoutUnit(GameManager gameManager)
    {
        this.gameManager = gameManager;

        //Debug.Log("生成開始");
        while (true)
        {
            timer++;

            if (gameManager.gameMode == GameManager.GameMode.Preparate && GenerateIntervalBool())
            {

                //Debug.Log("生成準備1");
                if (Input.GetMouseButton(0))
                {
                    //画面クリックした座標をRay型の変数へ登録
                    //Debug.Log("生成準備2");
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                    //rayが接触したオブジェクトの情報をRaycasthit型の変数へ登録
                    if (Physics.Raycast(ray, out RaycastHit hit))
                    {
                        AllyController unit = Instantiate(unitPrefab, hit.point, Quaternion.identity);
                        unit.transform.position = new Vector3(unit.transform.position.x, hit.point.y + 0.5f, unit.transform.position.z);

                        //生成したユニット用のリストに追加
                        gameManager.AllyUnitList.Add(unit);

                        timer = 0;

                    }
                }
            }
            yield return null;
        }    
    }

    /// <summary>
    /// ユニットの生成に待機時間を作るためのブーリアン
    /// </summary>
    /// <returns></returns>
    private bool GenerateIntervalBool()
    {
        if(timer >= gameManager.generateIntaervalTime)
        {
            return true;
        }
        else return false;       
    }
}