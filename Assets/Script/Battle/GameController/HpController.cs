using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// HPバー管理クラス
/// </summary>
public class HpController : MonoBehaviour
{
    [SerializeField] protected MobStatus status;
    [SerializeField] protected Slider hpSlider;

    // Update is called once per frame
    protected virtual void Update()
    {
        UpdateHPValue(hpSlider, status);
    }

    //現在のHP残量取得
    protected float GetHp(MobStatus status)
    {
        return status.Life;
    }

    //MAXHP量取得
    protected float GetMaxHp(MobStatus status)
    {
        return status.LifeMax;
    }

    //HPバーに表示するHP残量比率の計算・取得
    protected virtual void UpdateHPValue(Slider hpSlider, MobStatus status)
    {
        hpSlider.value = (float)GetHp(status) / (float)GetMaxHp(status);
    }
}
