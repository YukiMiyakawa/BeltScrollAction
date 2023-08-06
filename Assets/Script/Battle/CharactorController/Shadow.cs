using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �L�����N�^�[�̉e�Ǘ��N���X
/// </summary>
public class Shadow : MonoBehaviour
{
    private Transform transform;
    private float zPosition;
    [SerializeField] private GameObject body;
    [SerializeField] private MobStatus mobStatus;

    private void Start()
    {
        transform = GetComponent<Transform>();
    }

    public float ReturnZPosition()
    {
        return transform.position.z;
    }

    public float ReturnYPosition()
    {
        return transform.position.y;
    }

    /// <summary>
    /// �͈͍U���̃_���[�W���󂯂�ۂ̓L�����N�^�[�̉e���͈͓��ɂ��邩�ǂ����ł����蔻����s���B
    /// �{�N���X���A�^�b�`���ꂽ�e�X�v���C�g�����m�������̃��\�b�h���Ăяo���_���[�W��^����B
    /// </summary>
    /// <param name="i">�_���[�W���l</param>
    public void Damege(int i)
    {
        mobStatus.Damage(i);
    }
}
