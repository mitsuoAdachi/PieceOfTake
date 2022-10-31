using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AudioController : MonoBehaviour
{
    [SerializeField]
    AudioSource[] bgms;

    // Start is called before the first frame update
    void Start()
    {
        bgms[GameManager.stageLevel].Play();
        bgms[GameManager.stageLevel].DOFade(0.4f, 3);
    }
}
