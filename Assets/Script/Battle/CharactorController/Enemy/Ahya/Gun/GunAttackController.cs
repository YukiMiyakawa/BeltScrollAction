using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//çÏê¨ìríÜ
public class GunAttackController : MobConrtoller
{
    [SerializeField] GameObject attackObject;
    [SerializeField] GameObject attackRangeObject;
    [SerializeField] float attackSearchTime = 5f;
    [SerializeField] float attackRadyTime = 10f;
    [SerializeField] float attackOutTime = 15f;
    [SerializeField] Transform playerTransfrom;
    [SerializeField] Transform gunEnemyTransform;
    [SerializeField] EnemyController enemyController;
    [SerializeField] MobStatus gunEnemyStatus;

    Transform myTransForm;
    float attackTime;
    float attackAngle;
    Vector2 attackVelosity;
    float attackRangeaOutWaitTime;

    [SerializeField] Animator gunEffectAnimator;
    bool attackFlg = false;
    bool faceChengeAble = true;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        myTransForm = this.transform;
        attackRangeObject.SetActive(false);
        playerTransfrom = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("AhyaGunState: " + gunEnemyStatus.GetStateEnum());
        if (IsPause()) return;

        if (gunEnemyStatus.IsMovable)
        {
            GunAttackRoutine();
        }
        else
        {
            //Debug.Log("AttackRangeOff");
            attackTime = 0;
            attackRangeObject.SetActive(false);
            faceChengeAble = true;
        }

    }

    public void GunAttackRoutine()
    {
        attackTime += Time.deltaTime;
        attackRangeaOutWaitTime += Time.deltaTime;

        //myTransForm.position = new Vector3(myTransForm.position.x, myTransForm.position.y + 0.11f, myTransForm.position.z);

        if (attackTime < attackSearchTime)
        {
            enemyController.FaceChenge(enemyController.returnFaceVelosity());

            attackRangeObject.SetActive(true);
            attackFlg = true;
            float x = playerTransfrom.position.x - transform.position.x;
            float y = playerTransfrom.position.y - transform.position.y;

            attackVelosity = new Vector2(x, y).normalized;

            if (playerTransfrom.position.x > 0)
            {
                attackAngle = Mathf.Atan2(attackVelosity.y, attackVelosity.x) * Mathf.Rad2Deg;
                attackObject.transform.rotation = Quaternion.Euler(0, 0, attackAngle);
            }
            else
            {
                attackVelosity.x *= -1;
                attackAngle = Mathf.Atan2(attackVelosity.y, attackVelosity.x) * Mathf.Rad2Deg;
                attackObject.transform.rotation = Quaternion.Euler(0, 180, attackAngle);
            }

        }
        else if (attackTime < attackRadyTime)
        {
            faceChengeAble = false;
        }
        else if (attackTime < attackOutTime)
        {
            if (attackFlg)
            {
                attackRangeaOutWaitTime = 0;
                gunEffectAnimator.SetTrigger("Fire");
                attackFlg = false;
            }
            if (attackRangeaOutWaitTime > 1f) attackRangeObject.SetActive(false);
        }
        else
        {
            attackTime = 0;
            faceChengeAble = true;
        }
    }

    public bool GetFaceChengeAble()
    {
        return faceChengeAble;
    }

}
