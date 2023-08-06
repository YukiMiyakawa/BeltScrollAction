using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// �V�[���ύX�{�^�����ʃN���X
/// </summary>
public class SceneChangeButton : CommonCustomButtom
{
    SceneChangeController sceneChangeController;

    protected override void Start()
    {
        base.Start();
        sceneChangeController = GameObject.FindGameObjectWithTag("SceneChangeController").GetComponent<SceneChangeController>();
    }

    // �^�b�v  �{�^���̈���œ���A�����Ă��痣���܂ł̎��Ԃ���莞�ԓ��ł���B
    public override void ClickMethod()
    {
        Debug.Log("Click");
        if (SceneManager.GetActiveScene().name == nextSceneName.ToString())
        {
            Debug.Log("�V�[��������");
            return;
        }
        sceneChangeController.SceneChange(nextSceneName.ToString());
    }

}