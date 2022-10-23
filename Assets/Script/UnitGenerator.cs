using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
            gameManager.totalCost = Mathf.Clamp(gameManager.totalCost, 0, gameManager.stageDatas[GameManager.stageLevel].stageCost);

            if (gameManager.stageDatas[GameManager.stageLevel].stageCost > gameManager.totalCost)
            {
                if (gameManager.gameMode == GameManager.GameMode.Preparate)
                {
                    //　UI上ではRayが反応しないようにする
                    if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
                    {
                        //画面クリックした座標をRay型の変数へキャッシュ
                        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

                        //　rayが接触したオブジェクトの情報をRaycasthit型の変数へ登録
                        if (Physics.Raycast(ray, out RaycastHit hit))
                        {
                            UnitController allyUnit = Instantiate(gameManager.allyUnitDatas[uiManager.btnIndex].UnitPrefab, hit.point, Quaternion.identity);

                            //AudioSource.PlayClipAtPoint(allyUnit.gameManager.allyUnitDatas[uiManager.btnIndex].generateVoice, Camera.main.transform.position, 1f);
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
                            gameManager.totalCost += allyUnit.Cost;
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
            txtCostRatio.text = "Stage Cost  " + gameManager.totalCost.ToString() + " / " + gameManager.stageDatas[GameManager.stageLevel].stageCost.ToString();


            if (Input.GetMouseButton(0) && gameManager.gameMode == GameManager.GameMode.Preparate_Remove)
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray ,out RaycastHit hit) && hit.transform.gameObject.tag　==　"Ally")
                {
                    UnitController hitUnit = hit.transform.GetComponent<UnitController>();

                    Destroy(hit.transform.gameObject);

                    //リストから削除、トータルコストを減算
                    gameManager.GenerateAllyList.Remove(hitUnit);
                    gameManager.totalCost -= hitUnit.Cost;

                }
            }
            yield return null;
        }
    }
}