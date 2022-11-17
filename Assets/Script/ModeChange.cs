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

    [SerializeField]
    AudioClip playBtnAudio;

    private AudioController audioCon;

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

            txtGameMode.text = "GAME MODE：プレイ";

            //ボタン押下時の効果音
            AudioSource.PlayClipAtPoint(playBtnAudio, Camera.main.transform.position, 0.7f);

            //モードチェンジボタンを非アクティブにする
            btnModeChange.gameObject.SetActive(false);

            //ユニット選択パネルを非アクティブにする
            selectUnitPanel.SetActive(false);

            //ユニット設置モードボタンを押せない状態にする
            btnPreparateModeChange.interactable = false;

            //ステージのレイヤーを切り替える
            gameManager.stageGenerator.SwitchStageLayer();

            //BGMの切り替え
            audioCon.StopBgm(GameManager.stageLevel);
            audioCon.PlayBgm(audioCon.battleNumber);
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

            TextChangePreparateButton("Remove", new Color32(255, 31, 0, 150));

        }
        else
        {
            gameManager.gameMode = GameManager.GameMode.Preparate;

            TextChangePreparateButton("Put", new Color32(0, 246, 67, 150));

        }
    }

    public void SetupModechange(AudioController audioCon)
    {
        this.audioCon = audioCon;
    }

    private void TextChangePreparateButton(string btnName,Color32 color)
    {
        txtPreparateModeChangeButton.text = btnName;
        btnPreparateModeChange.image.color = color;
    }
}
