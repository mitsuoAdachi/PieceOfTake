using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitButton : MonoBehaviour
{
    [SerializeField]
    private Button buttonUnit;
    public Button ButtonUnit { get => buttonUnit; }


    [SerializeField]
    private Image unitImage;
    [SerializeField]
    private Text unitCostTxt;
    [SerializeField]
    private Text unitNameTxt;

    private int unitCost;


    public void SetupUnitButton(Sprite unitSprite,string unitName,int unitCost)
    {
        unitImage.sprite = unitSprite;
        unitCostTxt.text = unitCost.ToString();
        unitNameTxt.text = unitName;

        this.unitCost = unitCost;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="stageCost">残っているステージコスト</param>
    public void CheckActiveButton(int stageCost)
    {
        //残っているステージのコストとこのボタンのユニットのコストを比べて、コストが超えている場合にはボタンを押せなくする。

        //超えてない時はボタンが押せる
    }
}
