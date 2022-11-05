using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SkipButton : MonoBehaviour
{
    [SerializeField]
    private Text txtSkip;

    [SerializeField]
    private Button btnSkip;
    public Button BtnSkip  { get => btnSkip; }

    private Sequence sequence;

    //private TutorialController tController;

    // Start is called before the first frame update
    void Start()
    {
        FlashSkipButton();
        sequence.Restart();       
    }

    private void FlashSkipButton()
    {
            sequence = DOTween.Sequence()
            .Append(txtSkip.DOFade(0.85f, 0.5f)
            .SetDelay(0.1f))
            .SetLoops(-1, LoopType.Yoyo)
            .Pause()
            .SetAutoKill(false)
            .SetLink(gameObject);
    }
}
