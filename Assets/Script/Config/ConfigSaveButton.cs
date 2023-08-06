using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �ݒ�ύX���f���|�b�v�A�b�v��\������
/// </summary>
public class ConfigSaveButton : BlackCurtainController
{
    ConfigManager configManager;
    [SerializeField] Animator popAnimator;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        configManager = GameObject.FindObjectOfType<ConfigManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ClickMethod()
    {
        configManager.SaveConfig();
        CurtainFadeIn();
    }

    public override void MethodAfterFadeIn()
    {
        base.MethodAfterFadeIn();
        popAnimator.SetTrigger("PopIn");
    }

    public override void MethodAfterFadeOut()
    {
        base.MethodAfterFadeOut();
        popAnimator.SetTrigger("PopOut");
    }
}
