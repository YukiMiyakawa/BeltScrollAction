using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成途中
public class TuController : EnemyController
{
    [SerializeField] GameObject Tu;
    [SerializeField] Collider2D knifeCollider2D;

    //次行動フラグ
    enum NextBehavior
    {
        Normal, StepAttack, KnifeThrow, RainKnifeThrow
    }
    NextBehavior nextBehavior = NextBehavior.StepAttack;
    bool IsNextBehaviorNomal => nextBehavior == NextBehavior.Normal;
    bool IsNextBehaviorStepAttack => nextBehavior == NextBehavior.StepAttack;
    bool IsNextBehaviorKnifeThrow => nextBehavior == NextBehavior.KnifeThrow;
    bool IsNextBehaviorRainKnifeThrow => nextBehavior == NextBehavior.RainKnifeThrow;

    bool waitFlg = true;
    float waitTime = 0;
    float waitEndTime;
    string triggerName;

    Vector2 moveVelosity;
    float nomalWaitTime = 0;

    bool behaviorChangeFlg = true;

    [SerializeField] GameObject knife;
    [SerializeField] float knifeXspeed = 15;
    [SerializeField] float knifeYspeed = 15;
    Rigidbody2D knifeRb;

    [SerializeField] GameObject rainKnifeDown;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        knifeRb = knife.GetComponent<Rigidbody2D>();
        knife.SetActive(false);
        rainKnifeDown.SetActive(false);
        status.SetGuardStateToDefense();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPause()) return;

        if (enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("WaitState"))
        {
            waitTime += Time.deltaTime;
            if (waitFlg)
            {
                status.SetGuardStateToNormal();
                waitFlg = false;
            }
        }

        if(waitTime > 3f)
        {
            waitTime = 0;
            waitFlg = true;
            status.SetGuardStateToDefense();
            enemyAnimator.SetTrigger("UpMove");
        }

        if (enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("UpMove")) waitFlg = true;

        if (behaviorChangeFlg)
        {
            if (IsNextBehaviorStepAttack)
            {
                enemyAnimator.SetTrigger("Attack");
                nextBehavior = NextBehavior.KnifeThrow;
                behaviorChangeFlg = false;
            }
            else if (IsNextBehaviorKnifeThrow)
            {
                enemyAnimator.SetTrigger("KnifeThrow");
                nextBehavior = NextBehavior.RainKnifeThrow;
                behaviorChangeFlg = false;
            }
            else if (IsNextBehaviorRainKnifeThrow)
            {
                enemyAnimator.SetTrigger("RainKnifeThrow");
                nextBehavior = NextBehavior.StepAttack;
                behaviorChangeFlg = false;
            }
        }

        myRigidbody2D.velocity = moveVelosity;
        moveVelosity.y *= deceleration;
        moveVelosity.x *= deceleration;

        SmokeEffect();

    }

    public void WaitFlgOn()
    {
        waitFlg = true;
    }

    public void BehaviorFlgOn()
    {
        behaviorChangeFlg = true;
    }

    public void SetWaitTime(float endTime, string nextTriggerName)
    {
        waitEndTime = endTime;
        triggerName = nextTriggerName;
        waitFlg = true;
    }

    public void TriggerReset()
    {
        enemyAnimator.ResetTrigger("Attack");
    }

    public void StepAttackMove()
    {
        moveVelosity = GetStepMoveVelosity();
        FaceChenge(moveVelosity);
    }

    void VelosityReset()
    {
        myRigidbody2D.velocity = new Vector2(0, 0);
    }

    public void PositionChange()
    {
        if(-2 > player.transform.position.x || player.transform.position.x  > 2)
        {
            Vector2 tmpVector2;
            if (playerTransfrom.position.x < -2)
            {
                tmpVector2 = new Vector2(-1, 0);
            }
            else
            {
                tmpVector2 = new Vector2(1, 0);
            }

            TuPositionMove(0, 0, tmpVector2);
        }
        else if (playerTransfrom.position.x < 0)
        {
            TuPositionMove(3.7f, 0.4f, new Vector2(-1, 0));
        }
        else
        {
            TuPositionMove(-3.7f, 0.4f, new Vector2(1, 0));
        }
    }

    void TuPositionMove(float x, float y, Vector2 faceChangeVector2)
    {
        FaceChenge(faceChangeVector2);
        Transform tuTransform = Tu.transform;
        Vector3 pos = tuTransform.position;
        pos.x = x;
        pos.y = y;

        Tu.transform.position = pos;
        enemyAnimator.SetTrigger("DownMove");
        //Tu.SetActive(true);
    }

    public void KnifeThrow()
    {
        Transform knifetransform = knife.transform;
        Vector3 pos = knifetransform.position;
        pos.x = Tu.transform.position.x + 0.5f;
        pos.y = Tu.transform.position.y;
        knife.transform.position = pos;

        knife.SetActive(true);

        float x = playerTransfrom.position.x - knifetransform.position.x;
        float y = playerTransfrom.position.y - knifetransform.position.y + 0.5f;

        var tmpVelosity = new Vector2(x , y).normalized;
        tmpVelosity.x *= knifeXspeed;
        tmpVelosity.y *= knifeYspeed;

        FaceChenge(tmpVelosity);

        if (tmpVelosity.x > 0)
        {
            knife.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            knife.transform.eulerAngles = new Vector3(0, 180, 0);
        }

        knifeCollider2D.isTrigger = true;
        knifeRb.velocity = tmpVelosity;
    }

    public void RainKnifeDownPositionChange()
    {
        Transform rainKnifeTransform = rainKnifeDown.transform;
        Vector3 pos = rainKnifeTransform.position;
        pos.x = player.transform.position.x;
        pos.y = player.transform.position.y;
        rainKnifeDown.transform.position = pos;
    }

    public void OnHitRangeAttack(Collider2D collider)
    {
        var targetMob = collider.GetComponentInChildren<Shadow>();
        Debug.Log("AhyaGuntargetMob" + targetMob);
        if (null == targetMob) return;
        if (targetMob.transform.parent.gameObject.tag != "Player") return;

        targetMob.Damege(1);
    }

    public override void OnHitAttack(Collider2D collider)
    {
        var targetMob = collider.GetComponentInChildren<MobStatus>();
        Debug.Log("AhyaGuntargetMob" + targetMob);
        if (null == targetMob) return;
        //Debug.Log(AttackHitJudge(targetMob, status, 0.3f));
        if (AttackHitJudge(targetMob, status, 0.3f)) return;

        targetMob.Damage(1);
    }
}
