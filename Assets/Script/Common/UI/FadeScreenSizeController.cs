using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �t�F�[�h�A�E�g�A�|�b�v�A�b�v�\�����Ɏg�p����t�F�[�h�A�E�g�X�N���[���̃T�C�Y�����N���X
/// </summary>
public class FadeScreenSizeController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //FillScreen();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FillScreen()
    {
        var sr = GetComponent<RectTransform>();

        // �J�����̊O�g�̃X�P�[�������[���h���W�n�Ŏ擾
        float worldScreenHeight = Camera.main.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        //  ���҂̔䗦���o���ăX�v���C�g�̃��[�J�����W�n�ɔ��f
        sr.sizeDelta = new Vector2(worldScreenWidth, worldScreenHeight);

        // �J�����̒��S�ƃX�v���C�g�̒��S�����킹��
        Vector3 camPos = Camera.main.transform.position;
        camPos.z = 0;
        transform.position = camPos;
    }
}
