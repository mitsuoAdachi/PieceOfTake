using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LastStageButton : MonoBehaviour
{
    [SerializeField]
    private Button titleBtn;

    [SerializeField]
    private AudioClip btnAudio;

    // Start is called before the first frame update
    void Start()
    {
        titleBtn.onClick.AddListener(() => StartCoroutine(LoadTitleScene()));
    }

    private IEnumerator LoadTitleScene()
    {
        AudioSource.PlayClipAtPoint(btnAudio, Camera.main.transform.position, 1);

        GameManager.stageLevel++ ;

        yield return new WaitForSeconds(5);

        SceneManager.LoadScene("StartScene");
    }
}
