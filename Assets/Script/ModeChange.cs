using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeChange : MonoBehaviour
{
    private GameManager gameManager;

    //ゲームモードボタン用のメンバ変数群
    [SerializeField]
    private Button btnModeChange;

    [SerializeField]
    private Text txtGameMode;

    [SerializeField]
    private Text txtModeChangeButton;

    [SerializeField]
    private GameObject selectUnitPanel;

    //ユニット設置/削除ボタン用のメンバ変数群
    [SerializeField]
    private Button btnPreparateModeChange;
    public Button BtnPreparateModeChange { get => btnPreparateModeChange; }

    [SerializeField]
    private Text txtPreparateModeChangeButton;
    public Text TxtPreparateModeChangeButton { get => txtPreparateModeChangeButton; }


    /// <summary>
    /// メソッドをボタンに設定
    /// </summary>
    /// <param name="gameManager"></param>
    public void SetupChangeModeButton(GameManager gameManager)
    {
        this.gameManager = gameManager;

        btnModeChange.onClick.AddListener(() => ChangeGameMode());
        btnPreparateModeChange.onClick.AddListener(() => PreparateChangeGameMode());
    }

    /// <summary>
    /// ゲームモードの切り替え
    /// </summary>
    /// <param name="gameManager"></param>
    public void ChangeGameMode()
    {
        if (gameManager.gameMode != GameManager.GameMode.Play)
        {
            gameManager.gameMode = GameManager.GameMode.Play;

            txtModeChangeButton.text = "STOP";
            txtGameMode.text = "GAME MODE：プレイ";

            //ユニット選択パネルを非アクティブにする
            selectUnitPanel.SetActive(false);

            //ユニット設置モードボタンを押せない状態にする
            btnPreparateModeChange.interactable = false;
        }
        else
        {
            gameManager.gameMode = GameManager.GameMode.Stop;

            txtModeChangeButton.text = "PLAY";
            txtGameMode.text = "GAME MODE：ストップ";
        }
    }

    /// <summary>
    /// ユニット設置/削除モードの切り替え
    /// </summary>
    public void PreparateChangeGameMode()
    {
        if (gameManager.gameMode != GameManager.GameMode.Preparate_Remove)
        {
            gameManager.gameMode = GameManager.GameMode.Preparate_Remove;

            txtPreparateModeChangeButton.text = "Remove";
            btnPreparateModeChange.image.color = new Color32(255, 31, 0, 150);
        }
        else
        {
            gameManager.gameMode = GameManager.GameMode.Preparate_Remove;

            txtPreparateModeChangeButton.text = "Put";
            btnPreparateModeChange.image.color = new Color32(0, 246, 67, 150);
        }
    }

    //public void TextChange(Text btnText,string txt)
    //{
    //    btnText.text = txt;
    //}

    //public void TextColorChange(Button btn,Color32 col)
    //{
    //    btn.image.color = col;
    //}
}
