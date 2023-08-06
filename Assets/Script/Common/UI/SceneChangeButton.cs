using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// シーン変更ボタン共通クラス
/// </summary>
public class SceneChangeButton : CommonCustomButtom
{
    SceneChangeController sceneChangeController;

    protected override void Start()
    {
        base.Start();
        sceneChangeController = GameObject.FindGameObjectWithTag("SceneChangeController").GetComponent<SceneChangeController>();
    }

    // タップ  ボタン領域内で動作、押してから離すまでの時間が一定時間内である。
    public override void ClickMethod()
    {
        Debug.Log("Click");
        if (SceneManager.GetActiveScene().name == nextSceneName.ToString())
        {
            Debug.Log("シーン名同じ");
            return;
        }
        sceneChangeController.SceneChange(nextSceneName.ToString());
    }

}