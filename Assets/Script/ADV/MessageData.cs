using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ADVシーンcsv読み取りデータ構造定義クラス
/// </summary>
public class MessageData
{
    public string messageSide;
    public string charaName;
    public string charaExpress;
    public string charaMove;
    public string nameBg;
    public string message;
    public float afterWaitTimer;
    public int BgId = -1; 
    public string bgmName;
    public string seName;
    public bool isNextSceneProcess;

    public MessageData(string messageSide ="", string charaName = "", string charaExpress = "", string charaMove = "",string nameBg = "", string messege = "",
        float afterWaitTimer = -1, int BgId = 0, string bgmName = "", string seName = "", bool isNextSceneProccess = false)
    {
        this.messageSide = messageSide;
        this.charaName = charaName;
        this.charaExpress = charaExpress;
        this.charaMove = charaMove;
        this.nameBg = nameBg;
        this.message = messege;
        this.afterWaitTimer = afterWaitTimer;
        this.BgId = BgId;
        this.bgmName = bgmName;
        this.seName = seName;
        this.isNextSceneProcess = isNextSceneProccess;
    }
}
