using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ADVシーンにおいて、タップ操作でキャラクターの会話吹き出しオブジェクトを生成するクラス
/// ポップアップ画面が表示されている時、一定距離スワイプした時は生成されないよう制御する
/// </summary>
public class MessaeCreateController : MonoBehaviour
{
    Vector2 downPosition;　//タップ時の座標
    Vector2 upPosition; //タップを話した時の座標

    bool msgCreateFlg;

    NoticePanelController noticePanelController;
    GameSceneDirector gameSceneDirector;
    GameObject scrollView;

    float xArea, yArea; //タップ時、吹き出し生成を受け付ける座標を格納
    Vector3 scrollViewPos; //

    // Start is called before the first frame update
    void Start()
    {
        noticePanelController = GameObject.FindGameObjectWithTag("NoticePanel").GetComponent<NoticePanelController>();
        gameSceneDirector = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameSceneDirector>();
        scrollView = GameObject.FindGameObjectWithTag("ADVScrollView");
        GetAbleInputArea();
    }

    // Update is called once per frame
    void Update()
    {
        //タップ座標を取得
        if (Input.GetMouseButtonDown(0) && noticePanelController.isCurtainStateOfFadeOut)
        {
            if (JudgeAbleInputArea(Input.mousePosition))
            {
                downPosition = Input.mousePosition;
            }
        }
        //タップを離した時の座標を取得
        else if (Input.GetMouseButtonUp(0))
        {
            upPosition = Input.mousePosition;
            msgCreateFlg = true;
        }

        if (msgCreateFlg)
        {
            //一定距離以上スワイプされていないかを判定
            float dis = Vector2.Distance(downPosition, upPosition);
            if(dis < 0.1f)
            {
                gameSceneDirector.MessageCreate();
            }
            msgCreateFlg = false;
        }
    }

    /// <summary>
    /// 吹き出しオブジェクトを生成するスクロールエリアを取得する。
    /// </summary>
    void GetAbleInputArea()
    {
        scrollViewPos = scrollView.transform.position;
        var rectTrn = scrollView.GetComponent<RectTransform>();
        xArea = rectTrn.sizeDelta.x/2 + scrollViewPos.x;
        yArea = rectTrn.sizeDelta.y/2 + scrollViewPos.y;
    }

    /// <summary>
    /// タップした座標が画面下部の吹き出し生成箇所であるかを判定
    /// </summary>
    /// <param name="inputPosition"></param>
    /// <returns></returns>
    bool JudgeAbleInputArea(Vector2 inputPosition)
    {
        if(-xArea < inputPosition.x && inputPosition.x < xArea && -yArea < inputPosition.y && inputPosition.y < yArea)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
