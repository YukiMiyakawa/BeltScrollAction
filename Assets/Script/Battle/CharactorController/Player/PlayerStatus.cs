using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーキャラクターステータス制御クラス
/// 現在プレイヤーキャラGico固有のメソッドが混在しているため
/// 共通部分と固有部分を切り分けて、固有部分をGicoStatusクラスに移して継承させる予定
/// </summary>
public class PlayerStatus : MobStatus
{
    [SerializeField] private GameBehavior gameBehavior;
    //[SerializeField] private Collider2D attackCollider;
    [SerializeField] private Collider2D counterCollider; 

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Damage(int damage)
    {
        if (state == StateEnum.Die || guardState == GuardStateEnum.Defense) return;

        life -= damage;
        renderer.sortingOrder = charactorSortingGroup.sortingOrder + 1;
        hitEffect.Play();
        animator.SetTrigger("Idle");

        if (life > 0)
        {
            animator.SetTrigger("Damage");
            return;
        }
        else
        {
            animator.SetTrigger("Die");
        }

        OnDie();
        state = StateEnum.Die;
    }

    protected enum SpecialMoveStateEnum
    {
        Off, On
    }
    public string viewSpecialMoveEnum()       //<-　拡張メソッド
    {
        return specialState.ToString();
    }

    protected SpecialMoveStateEnum specialState = SpecialMoveStateEnum.Off;
    public bool IsSpecialMoveStateOn => SpecialMoveStateEnum.On == specialState;

    public void GoToSpecialOn()
    {
        specialState = SpecialMoveStateEnum.On;
    }

    public void GoToSpecialOff()
    {
        specialState = SpecialMoveStateEnum.Off;
    }

    public void CounterColliderOn()
    {
        counterCollider.enabled = true;
    }

    public void CounterColliderOff()
    {
        counterCollider.enabled = false;
    }

    public override void AllReset()
    {
        motionState = MotionStateEnum.Normal;
        guardState = GuardStateEnum.Nomal;
        if(attackCollider.enabled) attackCollider.enabled = false;
        if(counterCollider.enabled) counterCollider.enabled = false;
    }
}
