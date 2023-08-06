using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 背景シーン切り替えクラス
/// 削除予定
/// </summary>
public class SceneProcessIF : MonoBehaviour
{
    [SerializeField] protected int processMaxNum; //Sceneが動く回数
    protected int processNum;
    protected bool isProcess;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (isProcess)
        {
            NextSceneProcess();
        }
    }

    //trueになったら実行する流れを作る
    protected virtual void NextSceneProcess()
    {
        isProcess = false;
    }

    public void IsProcessOn()
    {
        isProcess = true;
    }
}
