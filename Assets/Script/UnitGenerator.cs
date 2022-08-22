using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitGenerator : MonoBehaviour
{
    private GameManager gameManager;

    private UIManager uiManager;

    [SerializeField]
    private UnitController allyPrefab;
    [SerializeField]
    private UnitController enemyPrefab;

    private UnitController allyUnit;
    private UnitController enemyUnit;

    [SerializeField]
    private Transform[] enemyTrans;

    //ユニット生成待機時間の初期値
    private int timer=100;

    /// <summary>
    /// クリックした位置にユニットを生成
    /// </summary>
    /// <param name="gameManager"></param>
    /// <returns></returns>
    public IEnumerator LayoutUnit(GameManager gameManager,UIManager uiManager)
    {
        this.gameManager = gameManager;
        this.uiManager = uiManager;

        //Debug.Log("生成開始");
        while (true)
        {
            timer++;

            if (gameManager.gameMode == GameManager.GameMode.Preparate && GenerateIntervalBool())
            {
                //UI上ではRayが反応しないようにする
                if (!EventSystem.current.IsPointerOverGameObject())
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
                            //Debug.Log("Ray座標" + hit.point);

                            allyUnit = Instantiate(allyPrefab, hit.point, Quaternion.identity);

                            //生成ユニットの位置調整
                            allyUnit.transform.position = new Vector3(allyUnit.transform.position.x, hit.point.y + 0.5f, allyUnit.transform.position.z);

                            //生成したユニットに移動能力を付与
                            allyUnit.StartMoveUnit(gameManager, gameManager.EnemyList);

                            //生成したユニットにステータスを付与
                            allyUnit.SetupUnitState(gameManager.allyUnitDatas,uiManager);

                            //生成したユニット用のリストに追加
                            gameManager.AllyList.Add(allyUnit);

                            timer = 0;
                        }
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
        if(timer >= gameManager.GenerateIntaervalTime)
            return true;
       else return false;       
    }

    /// <summary>
    /// 敵ユニットの生成
    /// </summary>
    public void PreparateEnemyUnit()
    {
        for (int i = 0; i < enemyTrans.Length; i++)
        {
            enemyUnit = Instantiate(enemyPrefab, enemyTrans[i], false);

            //生成したユニット用のリストに追加
            gameManager.EnemyList.Add(enemyUnit);

            //生成ユニットのサイズ調整
            enemyUnit.transform.SetParent(transform, false);

            //生成したユニットに移動能力を付与
            enemyUnit.StartMoveUnit(gameManager, gameManager.AllyList);

            //生成したユニットにステータスを付与
            enemyUnit.SetupUnitState(gameManager.enemyUnitDatas, uiManager);
        }
    }


    /// <summary>
    /// 生成したユニットの移動メソッドをセットアップ
    /// </summary>
    //private void SetupMoveUnit()
    //{
    //    StartCoroutine(unit.MoveUnit(gameManager, gameManager.EnemyList));
    //}

    //private bool GenerateRange()
    //{
    //    Vector3 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    //    float x = Mathf.Clamp(clickPos.x, -5, 5);
    //    float z = Mathf.Clamp(clickPos.z, -4.5f, 0);

    //    if (clickPos == new Vector3(x, 0, z))
    //        return true;
    //    else return false;
    //}
}