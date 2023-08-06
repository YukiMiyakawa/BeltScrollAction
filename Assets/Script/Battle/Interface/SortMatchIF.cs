using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// ���C���[����N���X
/// �G�t�F�N�g��ǉ������ۂ��̃N���X��p���ăL�����N�^�[�Ƃ��̃G�t�F�N�g�̃��C���[���𓯂��ɂ��鐧����s������
/// </summary>
public class SortMatchIF : MonoBehaviour
{
    [SerializeField] GameObject goj;
    [SerializeField] int layerNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.gameObject.GetComponent<SortingGroup>())
        {
            transform.gameObject.GetComponent<SortingGroup>().sortingOrder = goj.GetComponent<SortingGroup>().sortingOrder + layerNum;
        }
        else
        {
            var objSprite = goj.GetComponent<Renderer>();
            objSprite.sortingOrder = goj.GetComponent<SortingGroup>().sortingOrder + layerNum;
        }
    }
}
