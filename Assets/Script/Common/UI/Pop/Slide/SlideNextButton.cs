using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �X���C�h�|�b�v�A�b�v�̃y�[�W�A�b�v�{�^������N���X
/// </summary>
public class SlideNextButton : CommonCustomButtom
{
    SlidePopController controller;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        controller = GetComponentInParent<SlidePopController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ClickMethod()
    {
        base.ClickMethod();
        controller.NextPase();
    }
}
