using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class UnitGenerator : MonoBehaviour
{
    private GameManager gameManager;
    private UIManager uiManager;

    //RemoveLayoutUnit()で使用するメンバ変数
    [SerializeField]
    private Text txtCostRatio;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    /// <summary>
    /// クリックした位置にユニットを生成
    /// </summary>
    /// <param name="gameManager"></param>
    /// <returns></returns>
    public IEnumerator LayoutUnit(GameManager gameManager, UIManager uiManager)
    {
        this.gameManager = gameManager;
        this.uiManager = uiManager;

        //　配置したユニットを削除する機能の準備
        StartCoroutine(RemoveLayoutUnit());

        gameManager.totalCost = Mathf.Clamp(gameManager.totalCost, 0, gameManager.stageDatas[GameManager.stageLevel].stageCost);

        //　ステージコスト＞配置ユニットの総コストの間ループ
        while (true)
        {
            Debug.Log("ステージコスト"　+　gameManager.stageDatas[GameManager.stageLevel].stageCost);
            Debug.Log("トータルコスト" + gameManager.totalCost);
            Debug.Log("選択中のユニットコスト" + gameManager.allyUnitDatas[uiManager.btnIndex].cost);

            if (gameManager.stageDatas[GameManager.stageLevel].stageCost - gameManager.totalCost > gameManager.allyUnitDatas[uiManager.btnIndex].cost)
                //yield break;
            Debug.Log("コスト条件クリア");

            if (gameManager.gameMode != GameManager.GameMode.Preparate)
                yield break;
            Debug.Log("モード条件クリア");

            //　UI上ではRayが反応しないようにする
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                //画面クリックした座標をRay型の変数へキャッシュ
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

                //　rayが接触したオブジェクトの情報をRaycasthit型の変数へ登録
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    //TODO 46行if文をここに挟んでみる

                    UnitController allyUnit = Instantiate(gameManager.allyUnitDatas[uiManager.btnIndex].UnitPrefab, hit.point, Quaternion.identity);

                    AudioSource audio = allyUnit.gameObject.GetComponent<AudioSource>();
                    Debug.Log(audio);
                    audio.Play();

                    //生成したユニットに移動能力を付与
                    allyUnit.StartMoveUnit(gameManager, gameManager.GenerateEnemyList);

                    //生成したユニットにステータスを付与
                    allyUnit.SetupUnitStateAlly(gameManager.allyUnitDatas, uiManager);

                    //生成したユニット用のリストに追加
                    gameManager.GenerateAllyList.Add(allyUnit);

                    //生成したユニットのコスト値を加算
                    CostRatio(allyUnit.Cost);
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
            if (Input.GetMouseButton(0) && gameManager.gameMode == GameManager.GameMode.Preparate_Remove)
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray ,out RaycastHit hit) && hit.transform.gameObject.tag　==　"Ally")
                {
                    UnitController hitUnit = hit.transform.GetComponent<UnitController>();

                    Destroy(hit.transform.gameObject);

                    //リストから削除、トータルコストを減算
                    gameManager.GenerateAllyList.Remove(hitUnit);
                    CostRatio(-hitUnit.Cost);
                }
            }
            yield return null;
        }
    }

    /// <summary>
    /// コストの増減
    /// </summary>
    /// <param name="cost"></param>
    private void CostRatio(int cost)
    {
        gameManager.totalCost += cost;

        DisplayCostRatio();
    }

    /// <summary>
    /// コストの表示
    /// </summary>
    private void DisplayCostRatio()
    {
        txtCostRatio.text = "Stage Cost  " + gameManager.totalCost.ToString() + " / " + gameManager.stageDatas[GameManager.stageLevel].stageCost.ToString();

        gameManager.JudgeTotalCost();
    }
}