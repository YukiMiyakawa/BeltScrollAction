using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// お知らせ詳細データ構造定義クラス
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
