using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ADVシーンの背景切り替え制御クラス
/// </summary>
public class BgImageDirector : MonoBehaviour
{
    [SerializeField] Image blackoutCurtain;
    [SerializeField] float blackoutSpeed = 0.1f;
    public bool isBlackout;
    float alfa = 0;
    int blackoutStep = 0;

    [SerializeField] GameObject parentSceneObject; //各背景画像を子オブジェクトに含む親オブジェクト
    [SerializeField] GameObject[] scenes;　//各背景画像オブジェクトを格納する変数
    int sceneId = 0;
    int nextSceneId = 1;

    // Start is called before the first frame update
    void Start()
    {
        GetAllChildObject();

        //１つ目の背景画像以外非表示にする
        for(int i = 0; i < scenes.Length; i++)
        {
            if (i > 0)
            {
                scenes[i].SetActive(false);
            }
            else
            {
                scenes[i].SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isBlackout)
        {
            BlakoutSceneChange();
        }
    }

    /// <summary>
    /// ADVシーン背景チェンジメソッド
    /// </summary>
    /// <param name="nextSceneId"></param>
    public void BlakoutSceneChangeOn(int nextSceneId)
    {
        if(sceneId == nextSceneId)
        {
            Debug.LogError("指定するsceneIdが今のsceneIdと同じです。");
            isBlackout = false;
            return;
        }
        this.nextSceneId = nextSceneId;
        isBlackout = true;
    }

    //背景画像を切り替えないけど動きを進めるときはこのメソッドを使用する。
    //背景画僧一つ一つアニメーションを設定しフェードアウト～フェードインを行っているためTriggerをセットしている。
    //なるべくスクリプト上で管理したいためDOTweenで切り替え動作を行うようにする予定。
    public void SceneProceed()
    {
        var sceneProcess = scenes[sceneId].GetComponent<Animator>();
        if (!sceneProcess) return;

        sceneProcess.SetTrigger("Next");
    }

    //背景画像変更
    void BlakoutSceneChange()
    {
        if(blackoutStep == 0)
        {
            //フェードアウト
            alfa += blackoutSpeed;
            BlackoutSetAlpha(alfa);
            if(alfa >= 1)
            {
                blackoutStep = 1;
            }
        }
        else if(blackoutStep == 1)
        {
            //シーン切り替え
            scenes[nextSceneId].SetActive(true);
            scenes[sceneId].SetActive(false);
            sceneId = nextSceneId;
            blackoutStep = 2;
        }
        else if(blackoutStep == 2)
        {
            //フェードイン
            alfa -= blackoutSpeed;
            BlackoutSetAlpha(alfa);
            if (alfa <= 0)
            {
                blackoutStep = 0;
                isBlackout = false;
            }
        }
    }

    void BlackoutSetAlpha(float alfa)
    {
        blackoutCurtain.color = new Color(0,0,0,alfa);
    }

    /// <summary>
    /// 背景画像が設定されたオブジェクト群をscenes配列へ格納する。
    /// </summary>
    private void GetAllChildObject()
    {
        scenes = new GameObject[parentSceneObject.transform.childCount];

        for (int i = 0; i < parentSceneObject.transform.childCount; i++)
        {
            scenes[i] = parentSceneObject.transform.GetChild(i).gameObject;
        }
    }
}
