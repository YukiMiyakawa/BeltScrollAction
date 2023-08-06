using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���m�点�ڍ׃f�[�^�\����`�N���X
/// </summary>
public class NoticeBannerData
{
    public string noticeDate;
    public string noticeContent;
    public string noticeTitle;
    public string image;

    public NoticeBannerData(string noticeData, string noticeTitle, string noticeContent, string image = "")
    {
        this.noticeDate = noticeData;
        this.noticeTitle = noticeTitle;
        this.noticeContent = noticeContent;
        this.image = image;
    }
}
