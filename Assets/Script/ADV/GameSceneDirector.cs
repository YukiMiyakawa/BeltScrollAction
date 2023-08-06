using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// csvファイルからADVシーン管理クラス
/// </summary>
public class GameSceneDirector : SceneNameManager
{
    //csv読み込み
    [SerializeField] TextAsset csvFile;
    bool isSkip = true;　//csv一行目・ヘッダースキップ

    //メッセージオブジェクト
    List<MessageData> messageDatas;
    float waitTimer;
    int dataIndex = 0;
    MessageController leftCtrl;
    MessageController rightCtrl;

    [SerializeField] Transform messageLeftParent;
    [SerializeField] Transform messageRightParent;
    [SerializeField] GameObject prefabmessage;
    [SerializeField] CharactorImageControll leftSideChara;
    [SerializeField] CharactorImageControll rightSideChara;

    ScrollRect scrollRect;

    //BGM、SE
    BgImageDirector bgImageDirector;
    AudioManager audioManager;

    //ADVシーン終わりに映像を流す際こちらのsfにアタッチ
    [SerializeField] GameObject endCut;

    //Sceneチェンジ
    SceneChangeController sceneChangeController;
    const float NEXT_SCENE_WAIT_TIME_FOR_END_CUT = 7; //次のシーンに変わる前に暗転してメッセージを流す場合のwait時間
    const float NEXT_SCENE_WAIT_TIME_FOR_NO_END_CUT = 1;

    const string LEFT_SIDE = "Left";
    const string RIGHT_SIDE = "Right";

    const string FADE_IN = "FadeIn";
    const string FADE_OUT = "FadeOut";
    const string GRAY_IN = "GrayIn";
    const string GRAY_OUT = "GrayOut";

    //TapIcon
    [SerializeField] GameObject tapIcon;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;

        sceneChangeController = GameObject.FindGameObjectWithTag("SceneChangeController").GetComponent<SceneChangeController>();
        scrollRect = GameObject.Find("Scroll View").GetComponent<ScrollRect>();
        bgImageDirector = this.GetComponent<BgImageDirector>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        if (endCut) endCut.SetActive(false);

        messageDatas = new List<MessageData>();

        //Csv読み込み
        StringReader reader = new StringReader(csvFile.text);

        // , で分割しつつ一行ずつ読み込み
        // リストに追加していく
        while (reader.Peek() != -1) // reader.Peaekが-1になるまで
        {
            string line = reader.ReadLine(); // 一行ずつ読み込み
            string[] tmplists = line.Split(',');

            //一行目はスキップ
            if (isSkip)
            {
                isSkip = false;
                continue;
            }

            //csv型変換
            bool isNextSceneProcess;
            if (tmplists[10] == "TRUE")
            {
                isNextSceneProcess = true;
            }
            else
            {
                isNextSceneProcess = false;
            }

            float afterWaitTimer;
            if(tmplists[6] == "")
            {
                afterWaitTimer = -1;
            }
            else
            {
                afterWaitTimer = System.Convert.ToSingle(tmplists[6]);
            }

            int BgId;
            if(tmplists[7] == "")
            {
                BgId = 0;
            }
            else
            {
                BgId = System.Convert.ToInt32(tmplists[7]);
            }



            messageDatas.Add(new MessageData(tmplists[0], tmplists[1], tmplists[2], tmplists[3], tmplists[4], tmplists[5],
                afterWaitTimer, BgId, tmplists[8], tmplists[9], isNextSceneProcess));
        }

        leftSideChara.SetStartPosition();
        rightSideChara.SetStartPosition();

        //BGM再生
        if (messageDatas[0].bgmName != "")
        {
            audioManager.PlayBGM(messageDatas[0].bgmName);
        }

        MessageCreate();
    }

    // Update is called once per frame
    void Update()
    {
        waitTimer -= Time.deltaTime;

        //メッセージ生成終了時TAPアイコンを表示する。
        if (IsCreateMassage())
        {
            if (tapIcon) tapIcon.SetActive(true);
        }
        else
        {
            if (tapIcon) tapIcon.SetActive(false);
        }
    }

    /// <summary>
    /// 次メッセージを表示できる状態かboolで返すメソッド
    /// </summary>
    /// <returns></returns>
    bool IsCreateMassage()
    {
        if (waitTimer <= 0 && leftCtrl.isStateEnd && rightCtrl.isStateEnd && !leftSideChara.isMoveStateMove && !rightSideChara.isMoveStateMove)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 次メッセージを表示させようとする際に実行されるメソッド
    /// </summary>
    public void MessageCreate()
    {
        if (waitTimer > 0) return;

        if (messageDatas.Count - 1 < dataIndex)
        {
            //ストーリー終了時のメッセージを流すオブジェクトがアタッチされている場合は実行
            if (endCut)
            {
                endCut.SetActive(true);
                Invoke("SceneChange", NEXT_SCENE_WAIT_TIME_FOR_END_CUT);
            }
            else
            {
                Invoke("SceneChange", NEXT_SCENE_WAIT_TIME_FOR_NO_END_CUT);
            }
            Debug.Log("会話終了");
            return;
        }

        //前回のダイアログ表示を終わらせる
        if(leftCtrl != null && leftCtrl.isStatePlay)
        {
            leftCtrl.EndDialog();
            return;
        }
        if (rightCtrl != null && rightCtrl.isStatePlay)
        {
            rightCtrl.EndDialog();
            return;
        }

        //前回のキャラクターmoveが終わるまで待つ
        if (leftSideChara != null && leftSideChara.isMoveStateMove && leftSideChara.isCharaChange) return;
        if (rightSideChara != null && rightSideChara.isMoveStateMove && rightSideChara.isCharaChange) return;

        //メッセージ生成前に前のメッセージ吹き出し口を消す
        if (leftCtrl != null) leftCtrl.EraseSpeachOutlet();
        if (rightCtrl != null) rightCtrl.EraseSpeachOutlet();

        var msg = messageDatas[dataIndex++];
        //Debug.Log(msg.message);

        //会話スタート時キャラクターAtlasをセットする。
        if (leftSideChara.IsAtlasNullChack() && msg.messageSide == LEFT_SIDE)
        {
            leftSideChara.SetAtlas(msg.charaName, msg.charaExpress);
        }
        if (rightSideChara.IsAtlasNullChack() && msg.messageSide == RIGHT_SIDE)
        {
            rightSideChara.SetAtlas(msg.charaName, msg.charaExpress);
        }

        //背景画像切替
        if(msg.BgId > 0)
        {
            bgImageDirector.BlakoutSceneChangeOn(msg.BgId);
        }

        //背景画像動作
        if (msg.isNextSceneProcess)
        {
            bgImageDirector.SceneProceed();
        }

        //BGM再生・変更
        if (msg.bgmName != "" && (dataIndex -1) > 0)
        {
            audioManager.PlayBGM(msg.bgmName);
        }

        //SE再生
        if(msg.seName != "")
        {
            audioManager.PlaySE(msg.seName);
        }

        //話しているキャラクターを手前に出す、話していないキャラクターをGrayInする
        CharactorImageControll(msg);

        //キャラクターの表情変更
        if(msg.messageSide == LEFT_SIDE)
        {
            leftSideChara.FaceChange(msg.charaExpress);
        }
        else
        {
            rightSideChara.FaceChange(msg.charaExpress);
        }

        //ここでメッセージを生成する
        GameObject leftObj = Instantiate(prefabmessage, messageLeftParent);
        GameObject rightObj = Instantiate(prefabmessage, messageRightParent);

        leftCtrl = leftObj.GetComponent<MessageController>();
        rightCtrl = rightObj.GetComponent<MessageController>();
        leftCtrl.Init(msg.charaName, msg.message, Color.white, msg.messageSide, msg.nameBg);
        rightCtrl.Init(msg.charaName, msg.message, Color.white, msg.messageSide, msg.nameBg);

        //メッセージ生成時にスクロールを上に戻す
        scrollRect.verticalNormalizedPosition = 1.0f;

        waitTimer = msg.afterWaitTimer;
    }

    public Transform GetMessageTransform(string side)
    {
        if(side == LEFT_SIDE)
        {
            return messageLeftParent;
        }
        else if (side == RIGHT_SIDE)
        {
            return messageRightParent;
        }
        else
        {
            Debug.LogError("MessageSideTransformの指定が誤っています");
            return null;
        }
    }

    /// <summary>
    /// キャラクター立ち絵の動作を制御するメソッド
    /// ①キャラクターは手前に移動し、話していないキャラクターは後ろに下がりグレーアウトする
    /// ②キャラクターの立ち絵が変更される際はいったんフェードアウトし絵を変更後フェードインする
    /// </summary>
    /// <param name="msg"></param>
    void CharactorImageControll(MessageData msg)
    {
        //今設定されているキャラ名と次指定されているキャラ名が変化していたら立ち絵変更メソッドを実行
        if(msg.messageSide == LEFT_SIDE)
        {
            //シーン初めフェードインする
            if (leftSideChara.isPosStateFadeOut)
            {
                leftSideChara.PositionMove(FADE_IN);
            }
            //自身がグレイアウトしていたらフェードインする
            //自身のキャラクターが変化していたら入れ替わる
            //相手がグレイアウトしていなかったらグレイアウトする
            if(msg.charaName != "" && (leftSideChara.GetAtlasName() != msg.charaName))
            {
                leftSideChara.PositionMove("Change", msg.charaName, msg.charaExpress);
            }
            else if (leftSideChara.isPosStateGrayOut)
            {
                leftSideChara.PositionMove(GRAY_IN);
            }

            if (rightSideChara.isPosStateNormal)
            {
                rightSideChara.PositionMove(GRAY_OUT);
            }
            if (msg.charaExpress == null) return;
            leftSideChara.FaceChange(msg.charaExpress);
        }
        else if (msg.messageSide == RIGHT_SIDE)
        {
            if (rightSideChara.isPosStateFadeOut)
            {
                rightSideChara.PositionMove(FADE_IN);
            }

            if (msg.charaName != "" && (rightSideChara.GetAtlasName() != msg.charaName))
            {
                rightSideChara.PositionMove("Change", msg.charaName, msg.charaExpress);
            }
            else if (rightSideChara.isPosStateGrayOut)
            {
                rightSideChara.PositionMove(GRAY_IN);
            }

            if (leftSideChara.isPosStateNormal)
            {
                leftSideChara.PositionMove(GRAY_OUT);
            }
            if (msg.charaExpress == null) return;
            rightSideChara.FaceChange(msg.charaExpress);
        }
    }

    void SceneChange()
    {
        sceneChangeController.SceneChange(nextSceneName.ToString());
    }

    public void SceneSkipButton()
    {
        SceneChange();
    }
}
