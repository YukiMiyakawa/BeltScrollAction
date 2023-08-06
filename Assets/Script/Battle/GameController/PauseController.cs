using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Battleシーンの開始・停止・終了制御クラス
/// </summary>
public class PauseController : MonoBehaviour
{
    [SerializeField] protected GameObject slidePanel;
    [SerializeField] protected AudioManager audioManager;
    [SerializeField] protected string bgmName = "Battle1";
    [SerializeField] protected GameObject pauseUI;
    [SerializeField] protected GameObject gameOrverUI;
    [SerializeField] protected GameObject gameClearUI;
    [SerializeField] protected float startPauseTime = 3;
    protected bool isStart = true;

    [SerializeField] protected float gameTime = 600;
    [SerializeField] protected TextMeshProUGUI timeText;

    protected bool isPause = true;
    protected bool isGameOrver;
    protected bool isGameClear;

    protected GameObject[] enemyObjects;
    protected List<MobStatus> enemyStatuses = new List<MobStatus>();
    protected int enemyNumOfDieStatus;
    protected int enemyNum;


    protected MobStatus playerStatus;

    [SerializeField] protected Animator startAnimator;

    enum BgmStateEnum
    {
        Battle, Victory
    }

    BgmStateEnum bgmState = BgmStateEnum.Battle;
    public bool IsBattleBgm => BgmStateEnum.Battle == bgmState;
    public bool IsVictoryBgm => BgmStateEnum.Victory == bgmState;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        if(slidePanel) slidePanel.SetActive(false);

        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponentInChildren<AudioManager>();

        startAnimator.SetTrigger("Start");

        //UIオフ
        pauseUI.SetActive(false);
        gameOrverUI.SetActive(false);
        gameClearUI.SetActive(false);
        //敵ステータス取得
        enemyObjects = GameObject.FindGameObjectsWithTag("EnemyStatus");
        foreach(var enemyObject in enemyObjects)
        {
            enemyStatuses.Add(enemyObject.GetComponent<MobStatus>());
        }
        enemyNum = enemyStatuses.Count;

        //プレイヤーステータス取得
        playerStatus = GameObject.FindWithTag("PlayerStatus").GetComponent<MobStatus>();

        timeText.text = gameTime.ToString("f1");
        audioManager.PlayBGM($"{bgmName}");
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //バトルスタートアニメーション中はキャラクター動作制御を停止させる。
        if(startPauseTime >= 0)
        {
            startPauseTime -= Time.deltaTime;
            isPause = true;
            return;
        }
        else
        {
            if (isStart && PlayerDataManager.GetFirstFlg())
            //if (isStart)
            {
                isPause = false;
                isStart = false;
                PlayerDataManager.FirstBatlleOn();
                if (slidePanel) slidePanel.SetActive(true);
            }
        }

        //操作説明等のポップアップが表示されている間は画面動作を停止させる。
        if (slidePanel)
        {
            if (slidePanel.activeSelf)
            {
                Time.timeScale = 0;
                isPause = true;
            }
            else
            {
                isPause = false;
                Time.timeScale = 1;
            }
        }

        timeText.text = gameTime.ToString("f1");

        if (isPause || isGameClear || isGameOrver) return;

        //バトルタイム計測
        if(gameTime >= 0)
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


    public bool IsPause()
    {
        return isPause;
    }

    public bool IsGameOrver()
    {
        return isGameOrver;
    }

    public bool IsGameClear()
    {
        return isGameClear;
    }

    public void Pause()
    {
        Time.timeScale = 0.0f;
        isPause = true;
        pauseUI.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        isPause = false;
        pauseUI.SetActive(false);
    }

    /// <summary>
    /// ゲームオーバー画面表示
    /// </summary>
    public virtual void GameOrver()
    {
        isGameOrver = true;
        gameOrverUI.SetActive(true);
        //Animator animator = gameOrverUI.GetComponentInChildren<Animator>();
        //animator.SetTrigger("Next");
        audioManager.PlayBGM("Victory", false);
    }

    /// <summary>
    /// ゲームクリア画面表示
    /// </summary>
    public virtual void GameClear()
    {
        isGameClear = true;
        gameClearUI.SetActive(true);
        //Animator animator = gameClearUI.GetComponentInChildren<Animator>();
        //animator.SetTrigger("Next");
        bgmState = BgmStateEnum.Victory;
        audioManager.PlayBGM("Victory", false);
    }

    /// <summary>
    /// 敵を倒した数をカウントし、敵総数と同数になった場合ゲームクリア画面を表示する。
    /// </summary>
    public virtual void EnemyOfDieStatusCount()
    {
        enemyNumOfDieStatus++;
        if (enemyNumOfDieStatus == enemyNum) GameClear();
    }

    /// <summary>
    /// プレイヤーが倒されたらゲームオーバー画面を表示する。
    /// </summary>
    public virtual void IsPlayerStateOfDie()
    {
        if (playerStatus.IsDie) GameOrver();
    }

    protected virtual void TimeOver()
    {
        if (gameTime <= 0) GameOrver();
    }
}
