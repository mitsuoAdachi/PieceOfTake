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
    private AudioClip startAudio;

    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(OnLoadStageScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator LoadStageScene()
    {
        AudioSource.PlayClipAtPoint(startAudio, Camera.main.transform.position, 1f);
        startButton.transform.DOShakeScale(5);

        yield return new WaitForSeconds(3);

        SceneManager.LoadScene("StageScene");
    }

    public void OnLoadStageScene()
    {
        StartCoroutine(LoadStageScene());
    }

}
