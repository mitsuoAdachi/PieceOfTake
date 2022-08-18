using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{ 
    public Button[] unitSelectButtons;

    public int btnIndex;

    [SerializeField]
    private EventSystem eventSystem;

    /// <summary>
    /// ユニット選択ボタン押下時に各ボタンの番号を出力
    /// </summary>
    public void UnitSelect()
    {
        eventSystem.currentSelectedGameObject.TryGetComponent<ButtonIndex>(out var btn);
        //Debug.Log(btn.BtnIndex);
        btnIndex = btn.BtnIndex;
    }
}
