using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// お知らせ詳細データ構造定義クラス
/// </summary>

public class NoticeContentController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI noticeDateText;
    [SerializeField] TextMeshProUGUI noticeTitleText;
    [SerializeField] TextMeshProUGUI noticeContentText;

    public void Init(string date, string title, string content)
    {
        noticeDateText.text = date;
        noticeTitleText.text = title;
        noticeContentText.text = content;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
