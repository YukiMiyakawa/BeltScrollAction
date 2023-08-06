using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �L�����N�^�[�̃X�e�[�^�X�����Ԏw��ŕύX����N���X
/// �X�e�[�^�X�ύX�͌��݃A�j���[�V�����C�x���g�ōs���Ă���
/// �f�o�b�O���ʓ|�Ȃ��߃X�N���v�g��Ő��䂵����
/// �쐬�r��
/// </summary>
public class StatusControll : MonoBehaviour
{
    private MobStatus status;

    private float motionTime;
    private float motionProgressTime = 0;
    private bool defenseStartFlg = false;
    private float defenseTime;
    private float defenseStartTime = 0;
    private float defenseProgressTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        status = GetComponent<MobStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        if (status.IsMotionOfMotionState)
        {
            motionProgressTime += Time.deltaTime;
            if (motionTime < motionProgressTime)
            {
                status.SetMotionStateToNormal();
                motionProgressTime = 0;
            }
        }

        if (defenseStartFlg)
        {
            defenseProgressTime += Time.deltaTime;

            if (defenseProgressTime > defenseStartTime)
            {
                status.SetGuardStateToDefense();
            }

            if (defenseTime < defenseProgressTime)
            {
                status.SetGuardStateToNormal();
                defenseProgressTime = 0;
                defenseStartFlg = false;
            }
        }
    }

    public void StartMotionOfMotionState(float cnfTime)
    {
        motionTime = cnfTime;
        motionProgressTime = 0;
        status.SetMotionStateToMotion();
    }

    public void StartDefenseOfGuardState(float cnfTime, float cnfStartTime = 0)
    {
        defenseTime = cnfTime;
        defenseStartTime = cnfStartTime;
        defenseProgressTime = 0;
        defenseStartFlg = true;

    }
}