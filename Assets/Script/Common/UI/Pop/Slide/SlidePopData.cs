using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// スライドポップアップで表示するデータの定義クラス
/// </summary>
public class SlidePopData
{
    public string content;
    public string image;

    public SlidePopData(string content, string image = "")
    {
        this.content = content;
        this.image = image;
    }
}
