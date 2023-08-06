using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Animation中に流したいSE定義クラス
/// アニメーションイベントに設定する
/// </summary>
public class AnimationEventSeManager : CharaSeManeger
{
    public void SE_Dash()
    {
        SEPlay("Dash");
    }

    public void SE_SwordAttack1()
    {
        SEPlay("SwordAttack1");
    }

    public void SE_SwordAttack2()
    {
        SEPlay("SwordAttack2");
    }

    public void SE_SwordAttack3()
    {
        SEPlay("SwordAttack3");
    }

    public void SE_ShotAttack1()
    {
        SEPlay("ShotAttack1");
    }

    public void SE_KnifeThrow()
    {
        SEPlay("KnifeThrow");
    }

    public void SE_BowAttack()
    {
        SEPlay("BowAttack");
    }

    public void SE_Damage1()
    {
        SEPlay("Damage1");
    }

    public void SE_SpecialCut()
    {
        SEPlay("SpecialCut");
    }

    public void SE_Kiran1()
    {
        SEPlay("Kiran1");
    }
}
