using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[�ݒ���Ǘ��N���X
/// �쐬�r��
/// </summary>
public static class PlayerDataManager
{
    //�퓬�V�[���ڍs����ԏ��߂̑��������\�������邩�ۂ��̃t���O
    static bool firstBattleFlg = true;

    public static bool GetFirstFlg()
    {
        return firstBattleFlg;
    }
    public static void FirstBatlleOn()
    {
        firstBattleFlg = false;
    }

    //���ʐݒ�
    static float bgm = 0.7f, se = 0.5f;
}
