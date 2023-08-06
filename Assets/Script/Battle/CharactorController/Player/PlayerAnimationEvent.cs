using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーキャラクターのアニメージョンイベント設定クラス
/// </summary>
public class PlayerAnimationEvent : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private Collider2D attackCollider2D;
    [SerializeField] private Collider2D counterCollider2D;
    [SerializeField] private Collider2D charactorCollider2D;
    [SerializeField] private PlayerStatus status;
    [SerializeField] private Shadow shadow;

    //private MobStatus status;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void OnAttackStart()
    {
        //attackCollider2D.enabled = true;
    }

    public void OnAttackFinished()
    {
        //attackCollider2D.enabled = false;
        StartCoroutine(CooldownCoroutine());
        status.SetStateToNormal();
    }

    public void OnHitAttack(Collider2D collider)
    {
        //Debug.Log("OnHitAttackStart");
        var targetMob = collider.GetComponent<MobStatus>();
        //var mineShadowPosition = status.ReturnShadowPosition();
        if (null == targetMob) return;
        //Debug.Log(targetMob);
        if (Mathf.Abs(targetMob.shadow.ReturnZPosition() - shadow.ReturnZPosition()) > 1)
        {
            return;
        }

        //Debug.Log("OnHitAttackUp");
        targetMob.Damage(1);
    }


    private IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(attackCooldown);
        status.SetStateToNormal();
    }

}
