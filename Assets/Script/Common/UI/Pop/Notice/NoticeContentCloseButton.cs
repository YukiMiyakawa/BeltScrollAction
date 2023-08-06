using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// ���m�点�ڍ׃|�b�v�A�b�v�N���[�Y�{�^������N���X
/// </summary>

public class NoticeContentCloseButton : CommonCustomButtom
{
    Animator noticeContentAnimator;
    Animator noticePanelAnimator;

    void Start()
    {
        buttonMove = ButtonMove.ScaleDown;
        noticeContentAnimator = transform.parent.GetComponent<Animator>();
        noticePanelAnimator = GameObject.FindGameObjectWithTag("NoticePanel").GetComponent<Animator>();
    }

    public override void ClickMethod()
    {
        StartCoroutine("PopOutIn");
    }

    IEnumerator PopOutIn()
    {
        noticeContentAnimator.SetTrigger("PopOut");
        yield return new WaitForSeconds(0.15f);
        noticePanelAnimator.SetTrigger("PopIn");
    }
}
