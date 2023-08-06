using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������ɉ����Đ����o���̃T�C�Y��ύX���邽�߂̃X�N���v�g
/// �����r��
/// </summary>
public class ContentSizeController : MonoBehaviour
{
    [SerializeField] RectTransform content;

    RectTransform myRectTransform;

    // Start is called before the first frame update
    void Start()
    {
        myRectTransform = this.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        var sizeDelta = myRectTransform.sizeDelta;
        sizeDelta = new Vector2(content.sizeDelta.x, content.sizeDelta.y);
        myRectTransform.sizeDelta = sizeDelta;
    }
}
