using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class TutorialController : MonoBehaviour
{
    //private float intervalTime = 7;

    //[SerializeField]
    //private Text message;

    private SkipButton skipButton;

    [SerializeField]
    private SkipButton btnSkip;

    [SerializeField]
    private Transform btnSkipTran;

    //ステージ切り替え時の演出用変数
    [SerializeField]
    private Fade fade;

    [SerializeField]
    private AudioClip btnAudio;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadNextStageScene(73));

        GenerateSkipButton();

        //StartCoroutine(OnTutrial());
    }

    private IEnumerator LoadNextStageScene(int time)
    {
        yield return new WaitForSeconds(time);

        //最初のステージへ遷移する
        GameManager.stageLevel++;
        string SceneAgain = SceneManager.GetActiveScene().name;

        fade.FadeIn(3, () =>
        {
            fade.FadeOut(7);
        });

        SceneManager.LoadScene(SceneAgain);
    }

    //スキップボタンの生成
    private void GenerateSkipButton()
    {
        skipButton = Instantiate(btnSkip, btnSkipTran, false);

        OnClickSkipButton();
    }

    //スキップボタンの設定
    private void OnClickSkipButton()
    {
        skipButton.BtnSkip.onClick.AddListener(() => StartCoroutine(LoadNextStageScene(5)));
        skipButton.BtnSkip.onClick.AddListener(() => OnClickSound());
    }

    //ボタン押下時の音
    private void OnClickSound()
    {
        skipButton.transform.DOShakeScale(5);
        AudioSource.PlayClipAtPoint(btnAudio, Camera.main.transform.position, 1);
    }

    //public IEnumerator OnTutrial()
    //{
    //    //チュートリアルの文を順番に流す
    //    yield return new WaitForSeconds(intervalTime);
    //    message.text = "ステージには敵ユニットが設置されています。";
    //    yield return new WaitForSeconds(intervalTime);
    //    message.text = "敵ユニットに勝てそうなヒーローを選んで、ステージに設置しましょう。";
    //    yield return new WaitForSeconds(intervalTime);
    //    message.text = "点滅しているエリア内にしかヒーローは設置できません。";
    //    yield return new WaitForSeconds(intervalTime);
    //    message.text = "ヒーローによって設置コストが違います。ステージコストを超えるヒーローは設置できません。";
    //    yield return new WaitForSeconds(intervalTime);
    //    message.text = "Pullボタンを押してRemoveに切り替えることで設置したユニットをクリックでキャンセルすることができます。";
    //    yield return new WaitForSeconds(intervalTime);
    //    message.text = "プレイボタンを押すことでユニットは自動的に攻撃を開始します。";
    //    yield return new WaitForSeconds(intervalTime);
    //    message.text = "←→でカメラを回転、[Z][X]でズームアップ、ズームアウトができます。";
    //    yield return new WaitForSeconds(intervalTime);
    //    message.text = "敵ユニットを全て倒すことができればゲームクリアです。";
    //    yield return new WaitForSeconds(intervalTime);
    //    message.text = " ";
    //}


}
