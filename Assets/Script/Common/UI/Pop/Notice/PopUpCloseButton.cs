using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// お知らせ一覧ポップアップクローズ制御クラス
/// </summary>
public class PopUpCloseButton : CommonCustomButtom
{
    Animator popUpAnimator;
    NoticePanelController noticePanelController;

    protected override void Start()
    {
        base.Start();
        buttonMove = ButtonMove.ScaleDown;
        popUpAnimator = transform.parent.GetComponent<Animator>();
        noticePanelController = transform.parent.GetComponent<NoticePanelController>();
    }

    // タップ  
    public override void OnPointerClick(PointerEventData eventData) 
    {
        noticePanelController.CurtainFadeOut();
    }
}
