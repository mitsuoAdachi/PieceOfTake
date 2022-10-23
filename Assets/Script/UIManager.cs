using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    private GameManager gameManager;
    private ModeChange modeChange;
    
    //　UnitSelect()で使用するメンバ変数
    public int btnIndex;

    //　SetupUnitButton()で使用するメンバ変数群
    [SerializeField]
    private Button btnPrefab;
    [SerializeField]
    private Transform btnTran;

    public List<Button> unitSelectButtons = new List<Button>();

    /// <summary>
    /// ボタン押下時用のメソッド
    /// </summary>
    public void UnitSelect(int index)
    {
        //ボタンの配列番号を登録
        btnIndex = index;

        //ボタンを押したら配置モードがPreparateに戻るようにする
        gameManager.gameMode = GameManager.GameMode.Preparate;

        modeChange.TxtPreparateModeChangeButton.text = "Put";
        modeChange.BtnPreparateModeChange.image.color = new Color32(0, 246, 67, 150);
    }

    /// <summary>
    /// ユニットボタンの生成と各ボタン押下時の設定
    /// </summary>
    public void SetupUnitButton(GameManager gameManager,ModeChange modeChange)
    {
        this.gameManager = gameManager;
        this.modeChange = modeChange;

        //ユニットデータと同数のボタンを生成
        for (int i = 0; i < gameManager.allyUnitDatas.Count; i++)
        {
            Debug.Log("ボタン生成開始");

            //ボタン生成
            Button unitButton = Instantiate(btnPrefab, btnTran, false);
            //イメージの設定
            unitButton.image.sprite = gameManager.allyUnitDatas[i].unitImage;
            //コスト表記の設定
            Text cost = unitButton.transform.GetChild(0).GetComponent<Text>();
            cost.text = gameManager.allyUnitDatas[i].cost.ToString();
            //ユニットネームの設定
            Text name = unitButton.transform.GetChild(1).GetComponent<Text>();
            name.text = gameManager.allyUnitDatas[i].name.ToString();
            
            //リストに追加
            unitSelectButtons.Add(unitButton);

            //生成したボタンにUnitSelectメソッドと付与した順番のindexを登録
            int index = i;
            unitSelectButtons[i].onClick.AddListener(() => UnitSelect(index));
        }
    }

    public void OnDOTweenUI(Image ui)
    {
        ui.gameObject.SetActive(true);
        Tween tween;
        var sequence = DOTween.Sequence();
        tween = sequence.Append(ui.DOFade(1, 1).SetEase(Ease.OutCubic))
                        .Join(ui.transform.DOScale(6, 2).SetEase(Ease.OutBounce));
                      
    }
}
