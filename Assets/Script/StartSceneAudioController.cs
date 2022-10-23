using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartSceneAudioController : MonoBehaviour
{
    [SerializeField]
    AudioSource[] bgms;

    // Start is called before the first frame update
    void Start()
    {
        bgms[GameManager.stageLevel].DOFade(0.1f, 0.75f);
    }
}
