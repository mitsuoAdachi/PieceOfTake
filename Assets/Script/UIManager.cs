using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{ 
    public Button[] unitSelectButtons;

    public int btnIndex;

    /// <summary>
    /// ユニット選択ボタン押下時に各ボタンの番号を出力
    /// </summary>
    public void UnitSelect(int index)
    {
        btnIndex = index;
        //①各ボタンに識別用のスクリプトをアタッチして番号を取得するやり方
        //EventSystem.current.currentSelectedGameObject.TryGetComponent<ButtonIndex>(out var btn);
        //btnIndex = btn.BtnIndex;
        //Debug.Log(btnIndex);

        ////②ボタン要素を変数に代入しArray.IndexOfで配列番号を取得するやり方
        //Button element = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        //btnIndex = Array.IndexOf(unitSelectButtons, element);
        //Debug.Log(btnIndex);
    }

    /// <summary>
    /// 各種ボタン押下時の設定
    /// </summary>
    public void SetupUnitButton(int buttonCount)
    {
        //ユニット選択ボタンの設定
        for (int i = 0; i < buttonCount; i++)
        {
            //todo ボタン生成
            int index = i;
            unitSelectButtons[i].onClick.AddListener(() => UnitSelect(index));
        }
    }

}
