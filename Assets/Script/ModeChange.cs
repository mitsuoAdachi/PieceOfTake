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

    /// <summary>
    /// ゲームモードの切り替え
    /// </summary>
    /// <param name="gameManager"></param>
    public void GameModeChange(GameManager gameManager)
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
