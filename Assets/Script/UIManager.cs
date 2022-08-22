using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{ 
    public List<Button> unitSelectButtons = new List<Button>();

    public int btnIndex;

    [SerializeField]
    private Button btnPrefab;
    [SerializeField]
    private Transform btnTran;

    /// <summary>
    /// ボタン押下時にbtnIndexを出力
    /// </summary>
    public void UnitSelect(int index)
    {
        btnIndex = index;

        ////ボタン要素を変数に代入しArray.IndexOfで配列番号を取得する
        //Button element = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        //btnIndex = Array.IndexOf(unitSelectButtons, element);
        //Debug.Log(btnIndex);
    }

    /// <summary>
    /// ユニットボタンの生成と各ボタン押下時の設定
    /// </summary>
    public void SetupUnitButton(GameManager gameManager)
    {
        //ユニットデータ数分ボタンを生成
        for (int i = 0; i < gameManager.allyUnitDatas.Count; i++)
        {
            Debug.Log("ボタン生成開始");

            //ボタン生成
            Button unitButton = Instantiate(btnPrefab, btnTran, false);
            //リストに追加
            unitSelectButtons.Add(unitButton);
            //生成したボタンにUnitSelectメソッドと付与した順番のindexを登録
            int index = i;
            unitSelectButtons[i].onClick.AddListener(() => UnitSelect(index));
        }
    }

}
