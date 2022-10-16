using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StageNumberDisplay : MonoBehaviour
{
    [SerializeField]
    private Text stageNumbertxt;

    private GameManager gameManager;

    private void OnEnable()
    {
        if(TryGetComponent(out gameManager) == true)
        {
            Debug.Log(gameManager);
        }

        DisplayStageNumber();
    }

    public void DisplayStageNumber()
    {

        stageNumbertxt.DOFade(1, 0.001f);

        stageNumbertxt.text = "Stage" + gameManager.stageLevel.ToString();

        stageNumbertxt.DOFade(0, 5);

    }

    //public void SetupStageNumberDisplay(GameManager gameManager)
    //{
    //    this.gameManager = gameManager;

    //    DisplayStageNumber();
    //}


}
