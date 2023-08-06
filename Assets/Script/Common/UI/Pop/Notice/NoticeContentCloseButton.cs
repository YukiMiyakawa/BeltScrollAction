using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// お知らせ詳細ポップアップクローズボタン制御クラス
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
