using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ADV�V�[���̔w�i�𓮂����N���X
/// �폜�\��
/// </summary>
public class SceneProcessForTrigger : SceneProcessIF
{
    [SerializeField] Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void NextSceneProcess()
    {
        animator.SetTrigger("Next");
        base.NextSceneProcess();
    }
}
