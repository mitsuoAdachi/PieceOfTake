using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeChange : MonoBehaviour
{
    public Text txtMode;
    public Button btnModeChange;
    public Text txtModeChangeButton;

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
