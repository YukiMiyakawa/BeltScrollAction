using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// ���m�点�ꗗ�|�b�v�A�b�v�N���[�Y����N���X
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

    // �^�b�v  
    public override void OnPointerClick(PointerEventData eventData) 
    {
        noticePanelController.CurtainFadeOut();
    }
}
