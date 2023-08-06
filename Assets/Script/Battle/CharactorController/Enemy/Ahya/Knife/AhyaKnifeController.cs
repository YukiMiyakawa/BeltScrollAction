using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// AhyaKnifeの行動制御クラス
/// </summary>
public class AhyaKnifeController : EnemyController
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if(IsPause()) return;

        if (status.IsMovable)
        {
            MoveMethod();
        }
        else
        {
            myRigidbody2D.velocity = new Vector2(0, 0);
        }
    }

    public override void OnHitAttack(Collider2D collider)
    {

        var targetMob = collider.GetComponentInChildren<MobStatus>();
        if (null == targetMob) return;
        if (Mathf.Abs(targetMob.ReturnShadowPosition() - status.ReturnShadowPosition()) > 1)
        {
            return;
        }
        targetMob.Damage(1);
    }

    public void OnAttackRangeEnter(Collider2D collider)
    {
        if (collider.gameObject.tag != "Player") return;

        myRigidbody2D.velocity = new Vector2(0, 0);
        StartCoroutine(WaitTimeBeforeAttack());
    }

    IEnumerator WaitTimeBeforeAttack()
    {
        yield return new WaitForSeconds(2);

        enemyAnimator.SetTrigger("Attack");
    }

    public void Attack(GameObject target=null)
    {
        enemyAnimator.SetTrigger("Attack");
    }
}
