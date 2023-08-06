using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ADV�V�[���ɂ����āA�^�b�v����ŃL�����N�^�[�̉�b�����o���I�u�W�F�N�g�𐶐�����N���X
/// �|�b�v�A�b�v��ʂ��\������Ă��鎞�A��苗���X���C�v�������͐�������Ȃ��悤���䂷��
/// </summary>
public class MessaeCreateController : MonoBehaviour
{
    Vector2 downPosition;�@//�^�b�v���̍��W
    Vector2 upPosition; //�^�b�v��b�������̍��W

    bool msgCreateFlg;

    NoticePanelController noticePanelController;
    GameSceneDirector gameSceneDirector;
    GameObject scrollView;

    float xArea, yArea; //�^�b�v���A�����o���������󂯕t������W���i�[
    Vector3 scrollViewPos; //

    // Start is called before the first frame update
    void Start()
    {
        noticePanelController = GameObject.FindGameObjectWithTag("NoticePanel").GetComponent<NoticePanelController>();
        gameSceneDirector = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameSceneDirector>();
        scrollView = GameObject.FindGameObjectWithTag("ADVScrollView");
        GetAbleInputArea();
    }

    // Update is called once per frame
    void Update()
    {
        //�^�b�v���W���擾
        if (Input.GetMouseButtonDown(0) && noticePanelController.isCurtainStateOfFadeOut)
        {
            if (JudgeAbleInputArea(Input.mousePosition))
            {
                downPosition = Input.mousePosition;
            }
        }
        //�^�b�v�𗣂������̍��W���擾
        else if (Input.GetMouseButtonUp(0))
        {
            upPosition = Input.mousePosition;
            msgCreateFlg = true;
        }

        if (msgCreateFlg)
        {
            //��苗���ȏ�X���C�v����Ă��Ȃ����𔻒�
            float dis = Vector2.Distance(downPosition, upPosition);
            if(dis < 0.1f)
            {
                gameSceneDirector.MessageCreate();
            }
            msgCreateFlg = false;
        }
    }

    /// <summary>
    /// �����o���I�u�W�F�N�g�𐶐�����X�N���[���G���A���擾����B
    /// </summary>
    void GetAbleInputArea()
    {
        scrollViewPos = scrollView.transform.position;
        var rectTrn = scrollView.GetComponent<RectTransform>();
        xArea = rectTrn.sizeDelta.x/2 + scrollViewPos.x;
        yArea = rectTrn.sizeDelta.y/2 + scrollViewPos.y;
    }

    /// <summary>
    /// �^�b�v�������W����ʉ����̐����o�������ӏ��ł��邩�𔻒�
    /// </summary>
    /// <param name="inputPosition"></param>
    /// <returns></returns>
    bool JudgeAbleInputArea(Vector2 inputPosition)
    {
        if(-xArea < inputPosition.x && inputPosition.x < xArea && -yArea < inputPosition.y && inputPosition.y < yArea)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
