using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeChange : MonoBehaviour
{
    public Button btnModeChange;
    public Text txtModeChangeButton;

    public void GameModeChange(GameManager gameManager)
    {
        if (gameManager.gameMode != GameManager.GameMode.Play)
        {
            gameManager.gameMode = GameManager.GameMode.Play;

            txtModeChangeButton.text = "STOP";
        }
        else
        {
            gameManager.gameMode = GameManager.GameMode.Stop;

            txtModeChangeButton.text = "PLAY";
        }
    }
}
