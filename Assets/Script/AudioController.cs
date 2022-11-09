using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AudioController : MonoBehaviour
{
    [SerializeField]
    AudioSource[] bgms;

    public int battleNumber = 2;

    [SerializeField]
    ModeChange modeChange;

    // Start is called before the first frame update
    void Start()
    {
        PlayBgm(GameManager.stageLevel);
    }

    /// <summary>
    /// BGMを再生
    /// </summary>
    /// <param name="stageLelel"></param>
    /// <param name="volum"></param>
    /// <param name="duration"></param>
    public void PlayBgm(int index, float volum = 0.4f, float duration = 5.0f)
    {
        bgms[index].Play();
        bgms[index].DOFade(volum, duration);

        Debug.Log("BGM" + index);
        Debug.Log("ステージレベル" + GameManager.stageLevel);

        modeChange.SetupModechange(this);
    }

    /// <summary>
    /// BGMを停止
    /// </summary>
    /// <param name="stageLevel"></param>
    /// <param name="volum"></param>
    /// <param name="duration"></param>
    public void StopBgm(int stageLevel, float volum = 0f, float duration = 7.0f)
    {
        bgms[stageLevel].DOFade(volum, duration);
        DOVirtual.DelayedCall(5, () => bgms[stageLevel].Stop());        
    }

    public void SetupAudioController()
    {

    }
}
