using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private int unitCamIndex;

    [SerializeField]
    private Button unitCam;
    [SerializeField]
    private Button mainCam;

    /// <summary>
    /// ユニットカメラへ切り替えるボタンへ付与するメソッド
    /// </summary>
    private void ChangeUnitCam()
    {
        if (DataBase.instance.unitCamList.Count == 0)
            return;
        
        if (DataBase.instance.unitCamList.Count <= unitCamIndex)
        {
            unitCamIndex = 0;

            for (int i = 0; i < DataBase.instance.unitCamList.Count; i++)
            {
                DataBase.instance.unitCamList[i].gameObject.SetActive(false);
            }
        }
        else if (unitCamIndex == 0)
        {
            DataBase.instance.unitCamList[0].gameObject.SetActive(true);

            unitCamIndex++;
        }
        else if (unitCamIndex > 0)
        {
            DataBase.instance.unitCamList[unitCamIndex].gameObject.SetActive(true);

            unitCamIndex++;
        }    
    }

    private void ChangeMainCam()
    {
        unitCamIndex = 0;

        if (DataBase.instance.unitCamList == null)
            return;

        for (int i = 0; i < DataBase.instance.unitCamList.Count; i++)
        {
            DataBase.instance.unitCamList[i].gameObject.SetActive(false);
        }
    }

    public void SetupChangeCam()
    {
        unitCam.onClick.AddListener(() => ChangeUnitCam());
        mainCam.onClick.AddListener(() => ChangeMainCam());
    }

}
