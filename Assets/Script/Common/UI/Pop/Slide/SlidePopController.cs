using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// �X���C�h�|�b�v�A�b�v�̐���N���X
/// </summary>
public class SlidePopController : MonoBehaviour
{
    [SerializeField] GameObject slidePanel;

    [SerializeField] GameObject nextButton;
    [SerializeField] GameObject backButton;
 
    List<SlidePopData> datas;

    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI content;
    [SerializeField] TextMeshProUGUI pages;

    int totalPages;
    int currentPage = 0;

    // Start is called before the first frame update
    void Start()
    {
        datas = new List<SlidePopData>();

        //csv�œǂݎ��悤�ɉ��C�\��
        datas.Add(new SlidePopData("�X���C�v�������Ɉړ����܂��B�܂���苗�����̓G�����փX���C�v����Ǝ����ōU�����܂��B\n�A���œG�����֍U�����邱�Ƃő��i�U�����s���܂��I", "1111"));
        datas.Add(new SlidePopData("�{�^�����������Ƃŗl�X�ȃA�N�V�������s���܂��B\n���{�^���F\n�Z�b�g�����X�L���U�����s���܂��B�M�R�̏����X�L���̓J�E���^�[�U���ł��B", "2222"));
        datas.Add(new SlidePopData("��{���{�^���F\n�R���{���d�˂邱�ƂŃQ�[�W�����܂�A�Q�[�W100%�̏�Ԃŉ����ƕK�E��ԂɂȂ�܂��B�K�E��Ԃł͍U�����[�V�������ω����܂��B", "3333"));
        datas.Add(new SlidePopData("�ړ��{�^���F\n�ړ��{�^�����������ƂōU�����[�V�����̃I���I�t���s���܂��B�G�Ɉ͂܂ꂽ���Ȃǂ̓I�t�ɂ��ē����܂��傤�I", "1111"));

        totalPages = datas.Count - 1;

        SetInfo();
    }

    // Update is called once per frame
    void Update()
    {
        ButtonDisplay();
    }

    public void NextPase()
    {
        if(currentPage >= totalPages) slidePanel.SetActive(false);
        if(currentPage < totalPages) currentPage++;
        SetInfo();
    }

    public void BackPage()
    {
        if(currentPage > 0) currentPage--;
        SetInfo();
    }

    string GetPages()
    {   
        return $"{currentPage + 1}/{totalPages + 1}";
    }

    void SetInfo()
    {
        content.text = datas[currentPage].content;
        pages.text = GetPages();
    }

    /// <summary>
    /// ��Ԃ͂��߂̃y�[�W�̏ꍇ�̂݃y�[�W�_�E���{�^�����\���ɂ���
    /// </summary>
    void ButtonDisplay()
    {
        if(currentPage >= totalPages)
        {
            nextButton.SetActive(true);
            backButton.SetActive(true);
        }
        else if(currentPage <= 0)
        {
            nextButton.SetActive(true);
            backButton.SetActive(false);
        }
        else
        {
            nextButton.SetActive(true);
            backButton.SetActive(true);
        }
    }
}
