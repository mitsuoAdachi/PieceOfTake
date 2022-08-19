using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitGenerator : MonoBehaviour
{
    public List<UnitData> unitDatas = new List<UnitData>();

    private GameManager gameManager;

    //[SerializeField]
    //private LayerMask layerMask;

    [SerializeField]
    private UIManager uiManager;

    [SerializeField]
    private UnitController unitPrefab;

    private UnitController unit;

    //ユニット生成待機時間の初期値
    private int timer=100;

    /// <summary>
    /// クリックした位置にユニットを生成
    /// </summary>
    /// <param name="gameManager"></param>
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

                            unit = Instantiate(unitPrefab, hit.point, Quaternion.identity);

                            unit.transform.position = new Vector3(unit.transform.position.x, hit.point.y + 0.5f, unit.transform.position.z);

                            //生成したユニットが持つ移動用メソッドを呼び出す
                            StartCoroutine(unit.MoveUnit(gameManager, gameManager.EnemyList));

                            //生成したユニットにステータスを付与
                            StartCoroutine(unit.SetupUnitState(uiManager, this));

                            //生成したユニット用のリストに追加
                            gameManager.AllyList.Add(unit);

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
    /// ゲーム実行時にunitDataSO内のデータリストをunitDatasリストに収納し直す
    /// </summary>
    public void SetupUnitData()
    {
        for (int i = 0; i < DataBaseManager.instance.unitDataSO.unitDatasList.Count; i++)
        {
            unitDatas.Add(DataBaseManager.instance.unitDataSO.unitDatasList[i]);
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