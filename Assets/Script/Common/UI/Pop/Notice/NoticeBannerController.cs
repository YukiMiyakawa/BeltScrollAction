using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// お知らせ詳細ポップアップボタン制御クラス
/// </summary>

public class NoticeBannerController : CommonCustomButtom
{
    [SerializeField] TextMeshProUGUI noticeDateText;
    [SerializeField] TextMeshProUGUI noticeTitleText;
    [SerializeField] Image noticeImage;

    Animator noticePanelAnimator;
    Animator noticeContentAnimator;

    string noticeDate;
    string noticeContent;
    string noticeTitle;

    NoticeContentController noticeContentController;

    public void Init(string noticeDate, string noticeTitle, string noticeContent, string image = "")
    {
        noticeDateText.text = noticeDate;
        noticeTitleText.text = noticeTitle;
        this.noticeDate = noticeDate;
        this.noticeContent = noticeContent;
        this.noticeTitle = noticeTitle;
    }

    protected override void Start()
    {
        base.Start();
        buttonMove = ButtonMove.ScaleDown;

        noticeImage = GetComponentInChildren<Image>();
        noticePanelAnimator = GameObject.FindGameObjectWithTag("NoticePanel").GetComponent<Animator>();

        var noticeContentObj = GameObject.FindGameObjectWithTag("NoticeContent");

        noticeContentAnimator = noticeContentObj.GetComponent<Animator>();
        noticeContentController = noticeContentObj.GetComponent<NoticeContentController>();
    }


    // タップ  
    public override void OnPointerClick(PointerEventData eventData) 
    {
        noticeContentController.Init(noticeDate, noticeTitle, noticeContent);
        StartCoroutine("PopOutIn");
    }

    IEnumerator PopOutIn()
    {
        noticePanelAnimator.SetTrigger("PopOut");
        yield return new WaitForSeconds(0.15f);
        noticeContentAnimator.SetTrigger("PopIn");
    }
}
