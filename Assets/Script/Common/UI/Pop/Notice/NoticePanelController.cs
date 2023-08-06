using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// お知らせ一覧ポップアップ制御クラス
/// </summary>
public class NoticePanelController : BlackCurtainController
{
    Animator animator;

    [SerializeField] Transform scrollContent;
    [SerializeField] GameObject noticeBanner;
    [SerializeField] ScrollRect scrollRect;

    List<NoticeBannerData> noticeBannerDatas;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();

        if(scrollContent != null)
        {
            noticeBannerDatas = new List<NoticeBannerData>();

            //べた書きで書くお知らせを定義しているがcsvファイル読み込みで作成するように改修予定
            noticeBannerDatas.Add(new NoticeBannerData("1111.11.1", "test1", "test"));
            noticeBannerDatas.Add(new NoticeBannerData("1111.11.1", "test2", "test"));
            noticeBannerDatas.Add(new NoticeBannerData("1111.11.1", "test3", "test"));
            noticeBannerDatas.Add(new NoticeBannerData("1111.11.1", "test4", "test"));
            noticeBannerDatas.Add(new NoticeBannerData("1111.11.1", "test5", "test"));
            noticeBannerDatas.Add(new NoticeBannerData("1111.11.1", "test6", "test"));
            noticeBannerDatas.Add(new NoticeBannerData("1111.11.1", "test7", "test"));
            noticeBannerDatas.Add(new NoticeBannerData("1111.11.1", "test8", "test"));
            noticeBannerDatas.Add(new NoticeBannerData("1111.11.1", "test9", "test"));
            noticeBannerDatas.Add(new NoticeBannerData("1111.11.1", "test10", "test"));
            noticeBannerDatas.Add(new NoticeBannerData("1111.11.1", "test11", "test"));
            noticeBannerDatas.Add(new NoticeBannerData("1111.11.1", "test12", "test"));
            noticeBannerDatas.Add(new NoticeBannerData("1111.11.1", "test13", "test"));

            foreach (var noticeBannerData in noticeBannerDatas)
            {
                GameObject noticeBannerObj = Instantiate(noticeBanner, scrollContent);
                var noticeBannerController = noticeBannerObj.GetComponent<NoticeBannerController>();
                noticeBannerController.Init(noticeBannerData.noticeDate, noticeBannerData.noticeTitle, noticeBannerData.noticeContent);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void MethodAfterFadeIn()
    {
        base.MethodAfterFadeIn();
        animator.SetTrigger("PopIn");
        if(scrollRect) scrollRect.verticalNormalizedPosition = 1.0f;
    }

    public override void MethodAfterFadeOut()
    {
        base.MethodAfterFadeOut();
        animator.SetTrigger("PopOut");
    }

    public void ScrollTop()
    {
        if(scrollRect) scrollRect.verticalNormalizedPosition = 1.0f;
    }
}
