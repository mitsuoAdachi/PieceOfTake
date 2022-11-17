using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LoadScene : MonoBehaviour
{
    [SerializeField]
    private Button startButton;

    [SerializeField]
    private Button titleButton;

    [SerializeField]
    private Button againButton;

    [SerializeField]
    private Button nextButton;

    [SerializeField]
    private AudioClip pushAudio;

    [SerializeField]
    Fade fade;

    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(OnLoadStageScene);
        titleButton.onClick.AddListener(OnLoadStartScene);
        nextButton.onClick.AddListener(OnLoadNextStageScene);
        againButton.onClick.AddListener(OnLoadAgainScene);
    }

    public IEnumerator LoadStageScene(Button btn,string sceneName)
    {
        AudioSource.PlayClipAtPoint(pushAudio, Camera.main.transform.position, 1f);
        btn.transform.DOShakeScale(5);

        yield return new WaitForSeconds(2);

        fade.FadeIn(3, () =>
        {
            fade.FadeOut(7);
        });

        yield return new WaitForSeconds(3);

        SceneManager.LoadScene(sceneName);
    }

    public void OnLoadStageScene()
    {
        GameManager.stageLevel = 0;
        StartCoroutine(LoadStageScene(startButton,"StageScene"));
    }

    public void OnLoadStartScene()
    {
        StartCoroutine(LoadStageScene(titleButton,"StartScene"));
    }

    public void OnLoadAgainScene()
    {
        string SceneAgain = SceneManager.GetActiveScene().name;

        StartCoroutine(LoadStageScene(againButton, SceneAgain));
    }

    public void OnLoadNextStageScene()
    {
        string SceneAgain = SceneManager.GetActiveScene().name;

        GameManager.stageLevel++;

        StartCoroutine(LoadStageScene(againButton, SceneAgain));
    }
}
