using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���m�点�ꗗ�|�b�v�A�b�v����N���X
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

            //�ׂ������ŏ������m�点���`���Ă��邪csv�t�@�C���ǂݍ��݂ō쐬����悤�ɉ��C�\��
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
