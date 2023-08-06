using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// スライドポップアップの制御クラス
/// </summary>
public class SlidePopController : MonoBehaviour
{
    [SerializeField] GameObject slidePanel;

    [SerializeField] GameObject nextButton;
    [SerializeField] GameObject backButton;
 
    List<SlidePopData> datas;

    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI content;
    [SerializeField] TextMeshProUGUI pages;

    int totalPages;
    int currentPage = 0;

    // Start is called before the first frame update
    void Start()
    {
        datas = new List<SlidePopData>();

        //csvで読み取るように改修予定
        datas.Add(new SlidePopData("スワイプ下方向に移動します。また一定距離内の敵方向へスワイプすると自動で攻撃します。\n連続で敵方向へ攻撃することで多段攻撃が行えます！", "1111"));
        datas.Add(new SlidePopData("ボタンを押すことで様々なアクションを行えます。\n剣ボタン：\nセットしたスキル攻撃を行えます。ギコの初期スキルはカウンター攻撃です。", "2222"));
        datas.Add(new SlidePopData("二本剣ボタン：\nコンボを重ねることでゲージが溜まり、ゲージ100%の状態で押すと必殺状態になります。必殺状態では攻撃モーションが変化します。", "3333"));
        datas.Add(new SlidePopData("移動ボタン：\n移動ボタンを押すことで攻撃モーションのオンオフが行えます。敵に囲まれた時などはオフにして逃げましょう！", "1111"));

        totalPages = datas.Count - 1;

        SetInfo();
    }

    // Update is called once per frame
    void Update()
    {
        ButtonDisplay();
    }

    public void NextPase()
    {
        if(currentPage >= totalPages) slidePanel.SetActive(false);
        if(currentPage < totalPages) currentPage++;
        SetInfo();
    }

    public void BackPage()
    {
        if(currentPage > 0) currentPage--;
        SetInfo();
    }

    string GetPages()
    {   
        return $"{currentPage + 1}/{totalPages + 1}";
    }

    void SetInfo()
    {
        content.text = datas[currentPage].content;
        pages.text = GetPages();
    }

    /// <summary>
    /// 一番はじめのページの場合のみページダウンボタンを非表示にする
    /// </summary>
    void ButtonDisplay()
    {
        if(currentPage >= totalPages)
        {
            nextButton.SetActive(true);
            backButton.SetActive(true);
        }
        else if(currentPage <= 0)
        {
            nextButton.SetActive(true);
            backButton.SetActive(false);
        }
        else
        {
            nextButton.SetActive(true);
            backButton.SetActive(true);
        }
    }
}
