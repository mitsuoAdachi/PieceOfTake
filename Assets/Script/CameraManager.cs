using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField]
    private int unitCamIndex;

    [SerializeField]
    private Button unitCam;
    [SerializeField]
    private Button mainCam;

    /// <summary>
    /// ユニットカメラへ切り替えるボタンへ付与するメソッド
    /// </summary>
    /// <param name="gameManager"></param>
    private void ChangeUnitCam()
    {
        if (gameManager.unitCamList.Count == 0)
            return;
        
        if (gameManager.unitCamList.Count <= unitCamIndex)
        {
            unitCamIndex = 0;

            for (int i = 0; i < gameManager.unitCamList.Count; i++)
            {
                gameManager.unitCamList[i].gameObject.SetActive(false);
            }
        }
        else if (unitCamIndex == 0)
        {
            gameManager.unitCamList[0].gameObject.SetActive(true);

            unitCamIndex++;
        }
        else if (unitCamIndex > 0)
        {
            gameManager.unitCamList[unitCamIndex].gameObject.SetActive(true);

            unitCamIndex++;
        }    
    }

    private void ChangeMainCam()
    {
        unitCamIndex = 0;

        if (gameManager.unitCamList == null)
            return;

        for (int i = 0; i < gameManager.unitCamList.Count; i++)
        {
            gameManager.unitCamList[i].gameObject.SetActive(false);
        }
    }

    public void SetupChangeCam(GameManager gameManager)
    {
        this.gameManager = gameManager;

        unitCam.onClick.AddListener(() => ChangeUnitCam());
        mainCam.onClick.AddListener(() => ChangeMainCam());
    }

}
