using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public List<UnitButton> unitSelectButtons = new List<UnitButton>();

    private GameManager gameManager;
    private ModeChange modeChange;
    
    //　UnitSelect()で使用するメンバ変数
    public int btnIndex;

    //　SetupUnitButton()で使用するメンバ変数群
    [SerializeField]
    private UnitButton btnPrefab;
    [SerializeField]
    private Transform btnTran;

    [SerializeField]
    private Image blackPanel;

    private void Start()
    {
        StartCoroutine(OnBlackPanel());
    }

    /// <summary>
    /// ボタン押下時用のメソッド
    /// </summary>
    /// 
    public void UnitSelect(int index)
    {
        //ボタンの配列番号を登録
        btnIndex = index;

        Debug.Log("ボタンインデックス" + btnIndex);

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
        for (int i = 0; i < DataBase.instance.allyUnitDatas.Count; i++)
        {
            Debug.Log("ボタン生成開始");

            //ボタン生成
            UnitButton unitButton = Instantiate(btnPrefab, btnTran, false);

            unitButton.SetupUnitButton(DataBase.instance.allyUnitDatas[i].unitImage, DataBase.instance.allyUnitDatas[i].name, DataBase.instance.allyUnitDatas[i].cost);

            ////イメージの設定
            //unitButton.image.sprite = gameManager.allyUnitDatas[i].unitImage;
            ////コスト表記の設定
            //Text cost = unitButton.transform.GetChild(0).GetComponent<Text>();
            //cost.text = gameManager.allyUnitDatas[i].cost.ToString();
            ////ユニットネームの設定
            //Text name = unitButton.transform.GetChild(1).GetComponent<Text>();
            //name.text = gameManager.allyUnitDatas[i].name.ToString();
            
            //リストに追加
            unitSelectButtons.Add(unitButton);

            //生成したボタンにUnitSelectメソッドと付与した順番のindexを登録
            int index = i;
            unitSelectButtons[i].ButtonUnit.onClick.AddListener(() => UnitSelect(index));
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

    private IEnumerator OnBlackPanel()
    {
        blackPanel.DOFade(1, 0.01f);

        yield return new WaitForSeconds(0.5f);

        blackPanel.DOFade(0, 1f);
    }
}
