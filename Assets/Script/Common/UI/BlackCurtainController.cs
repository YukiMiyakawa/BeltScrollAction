using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BlackCurtainController : CommonCustomButtom
{
    [SerializeField] GameObject blackCurtain;
    Graphic blackCurtainGraphic;

    enum CurtainState
    {
        FadeIn, FadeOut
    }
    CurtainState curtainState = CurtainState.FadeOut;
    public bool isCurtainStateOfFadeOut => curtainState == CurtainState.FadeOut;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        base.Start();
        blackCurtainGraphic = blackCurtain.GetComponent<Graphic>();
        blackCurtain.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CurtainFadeIn()
    {
        //Debug.Log("FadeIn");
        blackCurtain.SetActive(true);
        curtainState = CurtainState.FadeIn;
        MethodAfterFadeIn();
        blackCurtainGraphic.DOFade(0.2f, 0.2f).OnComplete(() =>
        {
        });
    }

    public void CurtainFadeOut()
    {
        MethodAfterFadeOut();
        blackCurtainGraphic.DOFade(0, 0.2f).OnComplete(() =>
        {
            blackCurtain.SetActive(false);
            curtainState = CurtainState.FadeOut;
        });
    }

    public virtual void MethodAfterFadeIn()
    {

    }

    public virtual void MethodAfterFadeOut()
    {

    }
}
