using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeStage : MonoBehaviour
{
    public StageInfo stage;

    private GameManager gameManager;

    [SerializeField]
    Fade fade;

    public IEnumerator StageChange(GameManager gameManager)
    {
        this.gameManager = gameManager;

        gameManager.stageNumberDisplay.enabled = false;

        yield return new WaitForSeconds(3);

        fade.FadeIn(3, () =>
        {
            fade.FadeOut(7);
        });

        yield return new WaitForSeconds(4);

        Destroy(stage.gameObject);
        GameManager.stageLevel ++;
        gameManager.stageNumberDisplay.enabled = true;

        ResetStageSetting();

        gameManager.stageGenerator.PreparateStage(GameManager.stageLevel, gameManager);

    }

    private void ResetStageSetting()
    {
        for (int i = 0; i < gameManager.GenerateEnemyList.Count; i++)
        {
            Destroy(gameManager.GenerateEnemyList[i].gameObject);
        }
        gameManager.GenerateEnemyList.Clear();

        for (int i = 0; i < gameManager.GenerateAllyList.Count; i++)
        {
            Destroy(gameManager.GenerateAllyList[i].gameObject);
        }
        gameManager.GenerateAllyList.Clear();

    }

}
