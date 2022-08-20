using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeChange : MonoBehaviour
{
    [SerializeField]
    private Text txtMode;

    [SerializeField]
    private Button btnModeChange;
    public Button BtnModeChange { get => btnModeChange; }

    [SerializeField]
    private Text txtModeChangeButton;

    private GameManager gameManager;


    public void SetupModeChangeButton(GameManager gameManager)
    {
        this.gameManager = gameManager;

        //GameModeChangeメソッドをボタンに設定
        BtnModeChange.onClick.AddListener(() => GameModeChange());

    }

    /// <summary>
    /// ゲームモードの切り替え
    /// </summary>
    /// <param name="gameManager"></param>
    public void GameModeChange()
    {
        if (gameManager.gameMode != GameManager.GameMode.Play)
        {
            gameManager.gameMode = GameManager.GameMode.Play;

            txtModeChangeButton.text = "STOP";
            txtMode.text = "GAME MODE：プレイ";
        }
        else
        {
            gameManager.gameMode = GameManager.GameMode.Stop;

            txtModeChangeButton.text = "PLAY";
            txtMode.text = "GAME MODE：ストップ";
        }
    }
}
