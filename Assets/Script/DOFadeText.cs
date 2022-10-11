using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class DOFadeText : MonoBehaviour
{
    [SerializeField]
    private Image titleTxt;
    [SerializeField]
    private Image titleTTxt;

    [SerializeField]
    private Image startButton;

    private Vector3 titleDefaultPositon;
    private Vector3 startButtonDefaultPositon;

    // Start is called before the first frame update
    void Start()
    {
        //タイトルとスタートボタンを一旦消して下からフェードインする準備
        titleDefaultPositon = titleTxt.transform.localPosition;
        titleTxt.transform.localPosition = new Vector3(titleDefaultPositon.x, -10, 0);
        titleTxt.DOFade(0, 0);
        titleTTxt.DOFade(0, 0);

        startButtonDefaultPositon = startButton.transform.localPosition;
        startButton.transform.localPosition = new Vector3(startButtonDefaultPositon.x, -10, 0);
        startButton.DOFade(0, 0);

        StartCoroutine(OnStartSceneUI());   
    }

    private IEnumerator OnStartSceneUI()
    {
        var sequence = DOTween.Sequence();

        yield return new WaitForSeconds(2);

        titleTxt.DOFade(100, 1);
        titleTTxt.DOFade(100, 1);
        sequence.Append(titleTxt.transform.DOLocalMove(titleDefaultPositon, 1)) //TODO タイトルのTの文字だけ一定間隔毎にY軸で回転させる
                .Append(titleTTxt.transform.DORotate(Vector3.up * 180, 1)
                .SetDelay(3))
                .SetLoops(-1, LoopType.Restart);
       
        //.OnComplete(() =>
        //{
        //    sequence.Append(titleTTxt.transform.DORotate(Vector3.up * 180, 1)
        //    .SetDelay(3))
        //    .SetLoops(-1,LoopType.Yoyo);
        //});

        yield return new WaitForSeconds(3);

        startButton.DOFade(100, 1);
        startButton.transform.DOLocalMove(startButtonDefaultPositon, 1);

    }
}
