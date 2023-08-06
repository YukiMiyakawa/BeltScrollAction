using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//作成途中
public class AhyaGunController : EnemyController
{
    [SerializeField] GameObject attackObject;
    [SerializeField] GameObject attackRangeObject;
    [SerializeField] float attackSearchTime = 5f;
    [SerializeField] float attackRadyTime = 10f;
    [SerializeField] float attackOutTime = 15f;
    float attackTime;
    float attackAngle;
    Vector2 facekVelosity;

    [SerializeField] Animator gunEffectAnimator;
    bool attackFlg = false;

    Vector3 scale;

    [SerializeField] GunAttackController gunAttackController;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if(IsPause()) return;
        if(gunAttackController != null)
        {
            if (gunAttackController.GetFaceChengeAble())
            {
                base.Update();
            }
        }
        else
        {
            Debug.LogError("gunAttackControllerがアタッチされていません");
            base.Update();
        }
    }

    public override void OnHitAttack(Collider2D collider)
    {
        var targetMob = collider.GetComponentInChildren<Shadow>();
        Debug.Log("AhyaGuntargetMob" + targetMob);
        if (null == targetMob) return;
        //Debug.Log(AttackHitJudge(targetMob, status, 0.3f));
        //if (AttackHitJudge(targetMob, status, 0.3f)) return;

        targetMob.Damege(1);
    }
}
