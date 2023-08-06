using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 生成されたメッセージ吹き出し制御クラス
/// </summary>
public class MessageController : MonoBehaviour
{
    [SerializeField] Text textName;
    [SerializeField] Text textMassege;
    [SerializeField] Image nameBg;
    [SerializeField] Image message;
    [SerializeField] Image speachOutlet;

    string textMsg;
    string displayText;
    int textCharNumber;
    float nameAlfa;
    string messageSide;
    string thisMassegeSide;
    string nameBgColor;

    //会話テキスト再生ステータス
    public enum State
    {
        Play, End
    }
    public State state = State.Play;
    public bool isStatePlay => state == State.Play;
    public bool isStateEnd => state == State.End;

    public void Init(string name, string msg, Color bgColor, string massegeSide, string nameBgColor)
    {
        textName.text = name;
        textMsg = msg;
        nameBg.color = bgColor;
        this.messageSide = massegeSide;
        this.nameBgColor = nameBgColor;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetSpeachOutlet(this.messageSide);
        SetNameBgColor(nameBgColor);
        thisMassegeSide = this.transform.parent.gameObject.name;
        if(thisMassegeSide.Contains(messageSide))
        {
            Debug.Log(thisMassegeSide + "の" +　messageSide + "でメッセージ生成");
            StartCoroutine(Dialogue());
        }
        else
        {
            Debug.Log(thisMassegeSide + "の" + messageSide + "で空メッセージ生成");
            textName.color = new Color(1, 1, 1, 0);
            textMassege.color = new Color(1, 1, 1, 0);
            message.color = new Color(1, 1, 1, 0);
            nameBg.color = new Color(1, 1, 1, 0);
            speachOutlet.color = new Color(1, 1, 1, 0);
            state = State.End;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (textCharNumber >= textMsg.Length)
        {
            state = State.End;
        }
    }

    /// <summary>
    /// 会話テキスト表示メソッド
    /// 0.1秒毎テキストを一文字ずつ表示する
    /// </summary>
    /// <returns></returns>
    IEnumerator Dialogue()
    {
        if (textCharNumber < textMsg.Length) 
        {
            displayText = displayText + textMsg[textCharNumber];
            textCharNumber = textCharNumber + 1;
            textMassege.text = displayText;
            state = State.Play;
        }
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(Dialogue());
    }

    /// <summary>
    /// 会話テキスト全表示メソッド
    /// </summary>
    public void EndDialog()
    {
        //テキストを終わらせる
        if (state == State.End) return;

        textMassege.text = textMsg;
        textCharNumber = textMsg.Length;
        state = State.End;
    }

    /// <summary>
    /// 会話吹き出しオブジェクトが上から二つ目以降に移動した際吹き出し口を消す
    /// </summary>
    public void EraseSpeachOutlet()
    {
        speachOutlet.color = new Color(1, 1, 1, 0);
    }

    /// <summary>
    /// キャラ立ち絵の左右の位置で吹き出し口の位置を変更する
    /// </summary>
    /// <param name="messageSide"></param>
    void SetSpeachOutlet(string messageSide)
    {
        var speachOutletTransform = speachOutlet.transform;
        Vector3 speachOutletAngle = speachOutletTransform.localEulerAngles;
        if(messageSide == "Left")
        {
            speachOutlet.rectTransform.localPosition = new Vector3(-280, 75, 0);
        }
        else if(messageSide == "Right")
        {
            speachOutlet.rectTransform.localPosition = new Vector3(280, 75, 0);

        }
    }

    //ネームプレートの色変更
    void SetNameBgColor(string color)
    {
        switch (color)
        {
            case "blue":
                nameBg.color = new Color(0.5f,0.5f,1,1);
                break;
            case "red":
                nameBg.color = new Color(1,0.5f,0.5f,1);
                break;
            case "gray":
                nameBg.color = new Color(0.5f,0.5f,0.5f,1);
                break;
            case "magebta":
                nameBg.color = new Color(1, 0.5f,1,1);
                break;
            case "yellow":
                nameBg.color = new Color(1, 0.92f, 0.5f, 1);
                break;
            default:
                nameBg.color = new Color(0.5f, 0.5f, 0.5f, 1);
                return;
        }
    }
}
