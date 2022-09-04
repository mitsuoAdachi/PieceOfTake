using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UnitGenerator : MonoBehaviour
{
    private GameManager gameManager;
    private UIManager uiManager;

    public int generateTimer = 100;    //ユニット生成待機時間の初期値

    //// PreparateEnemyUnit()で使用するメンバ変数群
    //[SerializeField]
    //private UnitController enemyPrefab;

    [SerializeField]
    public List<Vector3> enemyTrans = new List<Vector3>();

    //RemoveLayoutUnit()で使用するメンバ変数
    [SerializeField]
    private Text txtCostRatio;

    private Camera mainCamera;

    //private Transform tran;

    void Start()
    {
        //tran = GetComponent<Transform>();
        mainCamera = Camera.main;
    }

    /// <summary>
    /// クリックした位置にユニットを生成
    /// </summary>
    /// <param name="gameManager"></param>
    /// <returns></returns>
    public IEnumerator LayoutUnit(GameManager gameManager,UIManager uiManager)
    {
        this.gameManager = gameManager;
        this.uiManager = uiManager;

        //　配置したユニットを削除する機能の準備
        StartCoroutine(RemoveLayoutUnit());

        //　ステージコスト＞配置ユニットの総コストの間ループ
        while (true)
        {
            //generateTimer++;

            gameManager.totalCost = Mathf.Clamp(gameManager.totalCost, 0, gameManager.stageDatas[gameManager.stageLv].stageCost);

            if (gameManager.stageDatas[gameManager.stageLv].stageCost > gameManager.totalCost)
            {
                if (gameManager.gameMode == GameManager.GameMode.Preparate)
                    //if (gameManager.gameMode == GameManager.GameMode.Preparate && GenerateIntervalBool())
                {
                    //　UI上ではRayが反応しないようにする
                    if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
                    {
                        //画面クリックした座標をRay型の変数へキャッシュ
                        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

                        //　rayが接触したオブジェクトの情報をRaycasthit型の変数へ登録
                        if (Physics.Raycast(ray, out RaycastHit hit))
                        {
                            //UnitController allyUnit = Instantiate(allyPrefab, hit.point, Quaternion.identity);
                            UnitController allyUnit = Instantiate(gameManager.allyUnitDatas[uiManager.btnIndex].UnitPrefab, hit.point, Quaternion.identity);

                            ////生成ユニットの位置調整
                            //allyUnit.transform.position = new Vector3(allyUnit.transform.position.x, hit.point.y + 0.5f, allyUnit.transform.position.z);

                            //生成したユニットに移動能力を付与
                            allyUnit.StartMoveUnit(gameManager, gameManager.EnemyList);

                            //生成したユニットにステータスを付与
                            allyUnit.SetupUnitState(gameManager.allyUnitDatas, uiManager);

                            //生成したユニット用のリストに追加
                            gameManager.AllyList.Add(allyUnit);

                            //生成したユニットのコスト値を加算
                            gameManager.totalCost += allyUnit.Cost;

                            //generateTimer = 0;                            
                        }
                    }
                }
            }
            yield return null;
        }    
    }

    /// <summary>
    /// Preparate_Removeモード時クリックしたユニットを削除する
    /// </summary>
    /// <returns></returns>
    public IEnumerator RemoveLayoutUnit()
    {
        while (true)
        {
            //uiManager.CostRatioTextChange();
            txtCostRatio.text = "Stage Cost  " + gameManager.totalCost.ToString() + " / " + gameManager.stageDatas[gameManager.stageLv].stageCost.ToString();


            if (Input.GetMouseButton(0) && gameManager.gameMode == GameManager.GameMode.Preparate_Remove)
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray ,out RaycastHit hit) && hit.transform.gameObject.tag=="Ally")
                {
                    UnitController hitUnit = hit.transform.GetComponent<UnitController>();

                    Destroy(hit.transform.gameObject);

                    //リストから削除、トータルコストを減算
                    gameManager.AllyList.Remove(hitUnit);
                    gameManager.totalCost -= hitUnit.Cost;

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
        if (generateTimer >= gameManager.GenerateIntaervalTime) return true;
        else return false;
    }


    /// <summary>
    /// 敵ユニットの生成
    /// </summary>
    //public void PreparateEnemyUnit()
    //{
    //    for (int i = 0; i < enemyTrans.Count; i++)
    //    {
    //        UnitController enemyUnit = Instantiate(gameManager.enemyUnitDatas[i].UnitPrefab, enemyTrans[i], Quaternion.identity);

    //        //生成したユニット用のリストに追加
    //        gameManager.EnemyList.Add(enemyUnit);

    //        //生成したユニットに移動能力を付与
    //        enemyUnit.StartMoveUnit(gameManager, gameManager.AllyList);

    //        //生成したユニットにステータスを付与
    //        enemyUnit.SetupUnitState(gameManager.enemyUnitDatas, uiManager);
    //    }
    //}

}