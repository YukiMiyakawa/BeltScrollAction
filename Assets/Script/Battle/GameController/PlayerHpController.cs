using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// プレイヤーのHPバー制御クラス
/// </summary>
public class PlayerHpController : HpController
{
    [SerializeField] Image hpBarFill;

    private const float DURATION = 0.3f;

    // Start is called before the first frame update
    protected void Start()
    {
        status = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<MobStatus>();
        hpBarFill.fillAmount = 1;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void UpdateHPValue(Slider hpSlider, MobStatus status)
    {
        hpBarFill.fillAmount = (float)GetHp(status) / (float)GetMaxHp(status);
    }
}
