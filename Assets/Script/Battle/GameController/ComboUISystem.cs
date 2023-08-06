using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// コンボ数表示制御クラス
/// 一定コンボ数を稼ぐごとに必殺ゲージを貯める
/// </summary>
public class ComboUISystem : MonoBehaviour
{
    [SerializeField] GameObject comboObj;
    [SerializeField] GameObject comboCnt;
    [SerializeField] AnimationCurve scaleCurve;
    [SerializeField, Range(0.2f, 0.5f)] float initEffectDuration = 0.2f; //初期演出時間
    [SerializeField, Range(0.01f, 0.1f)] float durationIncrement = 0.03f; //増加時間
    [SerializeField, Range(0.4f, 0.7f)] float maxEffectDuration = 0.4f; //最大演出時間
    [SerializeField, Range(1.2f, 1.5f)] float initMaxScale = 1.4f; //初期最大スケール
    [SerializeField, Range(0.2f, 1f)] float scaleIncrement = 0.6f; //スケール増加量
    [SerializeField, Range(5, 10)] float maxScale = 5f; //最大スケール
    [SerializeField, Range(0, 0.2f)] float basicScaleIncrement = 0.09f; //演出後の文字の大きさの増加量

    TextMeshProUGUI comboText;
    RectTransform comboRectTrans;

    int counter = 0;
    bool playingEffect = false;
    float scale;
    float basicScale;
    float effectDuration;
    float timer = 0f;
    Coroutine effectCol;
    Queue<int> comboOrder = new Queue<int>();

    PlayerStatus playerStatus;

    [SerializeField] int specialMoveMaxGauge = 100;
    [SerializeField] int specailMoveGaugePluseAmount = 10;
    private int specialMoveGauge = 0;

    void Awake()
    {
        //comboText = comboObj.GetComponent<Text>();
        comboText = comboCnt.GetComponent<TextMeshProUGUI>();
        comboRectTrans = comboCnt.GetComponent<RectTransform>();
        playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerStatus>();
    }

    private void Start()
    {
        Hide();
    }

    void Update()
    {
        if (!playerStatus.IsSpecialMoveStateOn && specialMoveGauge >= specialMoveMaxGauge)
        {
            specialMoveGauge = 0;
        }

        if (comboOrder.Count == 0) return;

        timer += Time.deltaTime;
        var tempRate = Mathf.Clamp((1f - counter / 10f), 0.3f, 0.5f); //コンボ数が大きいほど短時間で次の表示
        if (timer > effectDuration * tempRate)
        {
            timer = 0;
            UpdateCombo(comboOrder.Dequeue());
        }
    }

    /// <summary>
    /// コンボ数加算メソッド
    /// 引数は削除予定
    /// </summary>
    /// <param name="comboCnt"></param>
    public void IncreaseCombo(int comboCnt = 0)
    {
        counter++;

        SpecialGaugePlus(counter);

        comboOrder.Enqueue(counter);
        if (counter == 1) //初回のみ
            UpdateCombo(comboOrder.Dequeue());
    }

    /// <summary>
    /// コンボの更新
    /// </summary>
    /// <param name="comboCount">コンボ数</param>
    void UpdateCombo(int comboCount)
    {
        Show();
        comboText.text = comboCount.ToString();

        if (playingEffect)
        { //前のコンボ演出が終了してない場合
            StopCoroutine(effectCol);
            if (effectDuration < maxEffectDuration)
            {
                effectDuration += durationIncrement;
            }
            if (scale < maxScale)
            {
                scale += scaleIncrement;
            }
            if (counter < 7)
            { //7コンボまでは初期スケールを大きくする
                basicScale += basicScaleIncrement;
            }
        }
        else
        {
            scale = initMaxScale;
            basicScale = 1;
            effectDuration = initEffectDuration;
        }

        effectCol = StartCoroutine(PlayEffect(effectDuration));
    }

    //コンボ表示を見せる
    void Show()
    {
        comboObj.SetActive(true);
    }

    //コンボ表示を隠す
    void Hide()
    {
        comboObj.SetActive(false);
    }

    void Clear()
    {
        counter = 0;
        comboOrder.Clear();
        Hide();
    }

    /// <summary>
    /// 演出.テキストの大きさを大→小にする.
    /// </summary>
    /// <param name="duration">期間(sec)</param>
    IEnumerator PlayEffect(float duration)
    {
        var timer = 0f;
        var rate = 0f;
        var startScale = new Vector3(scale, scale, 1);
        var endScale = new Vector3(basicScale, basicScale, 1);

        playingEffect = true;
        while (rate < 1)
        {
            timer += Time.deltaTime;
            rate = Mathf.Clamp01(timer / duration); //Clamp01で値を0~1の間に制限している
            var curvePos = scaleCurve.Evaluate(rate);　//AnimationCurveの時間をrateで指定する
            comboRectTrans.localScale = Vector3.Lerp(startScale, endScale, curvePos);　//startScaleからendScaleまでの移動距離を1としcurvePosはどの地点に居るかを指定している。
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        Clear();

        playingEffect = false;
    }

    //=============================================================================================
    //必殺ゲージ管理メソッド
    //=============================================================================================

    /// <summary>
    /// 一定コンボ数を稼ぐごとに必殺ゲージを加算する
    /// 必殺ゲージが100%加算するとプレイヤーステータスを必殺技行使可能状態へと移行させる
    /// </summary>
    /// <param name="count">コンボ数</param>
    void SpecialGaugePlus(int count)
    {
        if (count % 2 == 0)
        {
            specialMoveGauge += specailMoveGaugePluseAmount;
            if (specialMoveGauge >= specialMoveMaxGauge)
            {
                playerStatus.GoToSpecialOn();
            }
        }
    }

    public int GetSpGauge()
    {
        return specialMoveGauge;
    }

    public int GetMaxSpGauge()
    {
        return specialMoveMaxGauge;
    }
}
