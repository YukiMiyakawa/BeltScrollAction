using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// キャラクターステータス制御基底クラス
/// </summary>
public class MobStatus : MonoBehaviour
{
    [SerializeField] protected ComboController comboController;
    [SerializeField] protected ParticleSystem hitEffect;
    [SerializeField] protected Collider2D attackCollider;
    [SerializeField] protected Collider2D rangeAttackCollider;
    protected PauseController pauseController;
    protected SortrLayerController sortLayerController;
    protected SortingGroup charactorSortingGroup;
    protected Renderer renderer;


    //キャラクラーステータス
    protected enum StateEnum
    {
        Nomal, Move, Attack, Die, Damage
    }

    protected StateEnum state = StateEnum.Nomal;
    public bool IsMovable => StateEnum.Nomal == state;
    public bool IsAttackable => StateEnum.Nomal == state;
    public bool IsDie => StateEnum.Die == state;

    public string GetStateEnum() 
    {
        return state.ToString();
    }

    //ガード状態ステータス
    protected enum GuardStateEnum
    {
        Nomal, Defense
    }
    public string GetGuardEnum()  
    {
        return guardState.ToString();
    }

    protected GuardStateEnum guardState = GuardStateEnum.Nomal;
    public bool IsDefenseOfGuardState => GuardStateEnum.Defense == guardState;

    //モーション状態ステータス　攻撃モーション中など入力を受け付けたくない時MotionEnumを使用
    protected enum MotionStateEnum
    {
        Normal, Motion
    }

    protected MotionStateEnum motionState = MotionStateEnum.Normal;
    public bool IsNormalOfMotionState => MotionStateEnum.Normal == motionState;
    public bool IsMotionOfMotionState => MotionStateEnum.Motion == motionState;

    public string GetMotionEnum()       //<-　拡張メソッド
    {
        return motionState.ToString();
    }

    //HPステータス
    [SerializeField] private float lifeMax = 10;
    protected float life;
    public float Life => life;
    public float LifeMax => lifeMax;

    protected　Animator animator;
    public Shadow shadow;

    protected virtual void Start()
    {
        pauseController = GameObject.FindGameObjectWithTag("GameManager").GetComponentInChildren<PauseController>();
        sortLayerController = GameObject.FindGameObjectWithTag("GameManager").GetComponentInChildren<SortrLayerController>();
        sortLayerController.addUnit(this.gameObject);
        renderer = hitEffect.GetComponent<Renderer>();
        hitEffect.Stop();
        charactorSortingGroup = this.GetComponent<SortingGroup>();
        life = lifeMax;
        animator = GetComponent<Animator>();
    }

    public virtual void Damage(int damage)
    {
        if(state == StateEnum.Die || guardState == GuardStateEnum.Defense) return;
        life -= damage;

        //hitパーティクルレイヤー変更
        renderer.sortingOrder = charactorSortingGroup.sortingOrder + 1;
        hitEffect.Play();

        //コンボプラスメソッド
        comboController.comboPlus();

        if (life > 0)
        {
            animator.SetTrigger("Damage");
            return;
        }

        state = StateEnum.Die;
        animator.SetTrigger("Die");
        OnDie();
    }

    protected virtual void OnDie()
    {
        StartCoroutine(DestroyCoroutine());
    }

    protected IEnumerator DestroyCoroutine()
    {
        pauseController.EnemyOfDieStatusCount();
        yield return new WaitForSeconds(2);
        Destroy(transform.root.gameObject);
    }

    //=========================================================================
    //ステータス変更メソッド
    //アニメーションイベントに設定するために定義
    //=========================================================================

    public void SetStateToNormal()
    {
        if (state == StateEnum.Die) return;
        state = StateEnum.Nomal;
    }

    public void SetStateToDamage()
    {
        state = StateEnum.Damage;
    }

    public void SetMotionStateToNormal()
    {
        motionState = MotionStateEnum.Normal;
    }

    public void SetMotionStateToMotion()
    {
        motionState = MotionStateEnum.Motion;
    }

    public void SetGuardStateToDefense()
    {
        guardState = GuardStateEnum.Defense;
    }

    public void SetGuardStateToNormal()
    {
        guardState = GuardStateEnum.Nomal;
    }

    public float ReturnShadowPosition()
    {
        return shadow.ReturnYPosition();
    }

    public void DefenseAndMotionOn()
    {
        motionState = MotionStateEnum.Motion;
        guardState = GuardStateEnum.Defense;
    }

    public void DefenseAndMotionOff()
    {
        motionState = MotionStateEnum.Normal;
        guardState = GuardStateEnum.Nomal;
    }

    public virtual void AllReset()
    {
        motionState = MotionStateEnum.Normal;
        guardState = GuardStateEnum.Nomal;
    }

    public virtual void AttackColliderOn()
    {
        attackCollider.enabled = true;
        attackCollider.isTrigger = true;
    }

    public virtual void AttackColliderOff()
    {
        attackCollider.enabled = false;
        attackCollider.isTrigger = false;
    }

    public virtual void RangeAttackColliderOn()
    {
        rangeAttackCollider.enabled = true;
        rangeAttackCollider.isTrigger = true;
    }

    public virtual void rangeAttackColliderOff()
    {
        rangeAttackCollider.enabled = false;
        rangeAttackCollider.isTrigger = false;
    }
}
