using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StageNumberDisplay : MonoBehaviour
{
    [SerializeField]
    private Text stageNumbertxt;

    private void Start()
    {
        DisplayStageNumber();
    }

    /// <summary>
    /// ステージ遷移演出時にステージナンバーを表示する
    /// </summary>
    public void DisplayStageNumber()
    {

        stageNumbertxt.DOFade(1, 0.001f);

        stageNumbertxt.text = DataBase.instance.stageDatas[GameManager.stageLevel].stageNumber.ToString();

        stageNumbertxt.DOFade(0, 20);

    }
}
