using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �L�����N�^�[���쐧����N���X
/// </summary>
public class MobConrtoller : MonoBehaviour
{
    protected PauseController pauseController;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        pauseController = GameObject.FindGameObjectWithTag("GameManager").GetComponentInChildren<PauseController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 2D�v���W�F�N�g�ɂ����ĉ��s�𑪂邽�߂ɍU�������݂��̉e��Y�|�W�V�����̋����𔻒肷��B
    /// </summary>
    /// <param name="targetStatus">�U�����q�b�g��������̃X�e�[�^�X</param>
    /// <param name="myStatus"></param>
    /// <param name="attackAbleDistance"></param>
    /// <returns></returns>
    protected virtual bool AttackHitJudge(MobStatus targetStatus, MobStatus myStatus, float attackAbleDistance)
    {
        float tmpDistance = Mathf.Abs(targetStatus.ReturnShadowPosition() - myStatus.ReturnShadowPosition());
        if (tmpDistance < attackAbleDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// ��ʃ|�[�Y��Ԃ��ǂ����𔻒�
    /// </summary>
    /// <returns></returns>
    public bool IsPause()
    {
        if (pauseController != null)
        {
            return pauseController.IsPause();
        }
        else
        {
            Debug.Log("MobPauseFalse");
            return false;
        }
    }
}
