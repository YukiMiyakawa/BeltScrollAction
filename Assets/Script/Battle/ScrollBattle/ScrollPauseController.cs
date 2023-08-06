using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// 無限スクロールバトルにおけるバトル開始・中断・終了制御クラス
/// </summary>
public class ScrollPauseController : PauseController
{
    [SerializeField] TextMeshProUGUI clearText;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        //バトルスタートアニメーション中はキャラクター動作制御を停止させる。
        if (startPauseTime >= 0)
        {
            startPauseTime -= Time.deltaTime;
            isPause = true;
            return;
        }
        else
        {
            isPause = false;
        }

        timeText.text = gameTime.ToString("f1");

        if (isPause || isGameClear || isGameOrver) return;

        //バトルタイム計測
        if (gameTime >= 0)
        {
            gameTime -= Time.deltaTime;
        }
        else
        {
            gameTime = 0;
        }

        TimeOver();
        IsPlayerStateOfDie();
    }

    public override void EnemyOfDieStatusCount()
    {
        enemyNumOfDieStatus++;
    }

    public override void GameOrver()
    {
        base.GameOrver();
    }

    public override void GameClear()
    {
        base.GameClear();
        clearText.text = $"GameClear\nたおしたかず　{enemyNumOfDieStatus}";
        isPause = true;
    }

    //無限スクロールステージではタイムオーバーでゲーム終了画面を呼ぶ
    protected override void TimeOver()
    {
        //base.TimeOver();
        if (gameTime <= 0) GameClear();
    }
}
