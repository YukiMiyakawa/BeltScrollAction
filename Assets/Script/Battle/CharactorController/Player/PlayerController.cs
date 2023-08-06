using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

/// <summary>
/// プレイヤーキャラクターコントロールクラス
/// 現在プレイヤーキャラGico固有のメソッドが混在しているため
/// 共通部分と固有部分を切り分けて、固有部分をGicoControllerクラスに移して継承させる予定
/// </summary>
public class PlayerController : MobConrtoller
{
    //向き変更オブジェクト
    GameObject thisBody;

    //移動
    protected float xMoveSpeed = 10f;
    protected float yMoveSpeed = 5f;
    protected float Deceleration = 0.9f;

    protected int faceAttackableMinAngle = -40;
    protected int faceAttackableMaxAngle = 40;
    protected int backAttackableMinAngle = -140;
    protected int backAttackableMaxAngle = 140;

    bool isMoveOnly;

    Vector2 tmpMoveVelosity;
    Vector2 moveVelosity;
    bool MoveFlg = false;
    Transform tmpEnemyTransform;

    bool IsOnClickSkillButton;
    bool IsOnClickSpecialButton;

    //スワイプ開始、終了地点
    [SerializeField] protected float attackMinDistance = 0.2f; 
    Vector2 startPosition;
    Vector2 finishPosition;
    bool SwipFlg = false;

    const float SWIP_ABLE_DISTANCE = 0.01f;

    //攻撃
    bool AttackFlg = false;
    Transform enemyTransform;

    //raycast
    [SerializeField] protected LayerMask layMask;
    [SerializeField] protected Transform layOriginPosition;
    protected float maxDistance = 2f;
    RaycastHit2D hit;
    RaycastHit2D arrowHit;
    Vector2 raycastVelosity;

    protected float AttackAbleDistance = 1.5f;

    //次行動フラグ
    protected enum NextBehavior
    {
        Normal, Move, Attack
    }
    NextBehavior nextBehavior = NextBehavior.Normal;

    bool IsNextBehaviorNormal => nextBehavior == NextBehavior.Normal;
    bool IsNextBehaviorMove => nextBehavior == NextBehavior.Move;
    bool IsNextBehaviorAttack => nextBehavior == NextBehavior.Attack;

    bool buttonDownFlg = false;
    bool buttonUpFlg = false;

    bool specialAttackFlg = false;
    float specialAttackTime = 100;　//必殺技ボタンが押されて必殺状態が維持される時間

    [System.Obsolete]

    [SerializeField]  Animator playerAnimator;　//プレイヤーキャラクターのAnimator

    [SerializeField]  PlayerStatus status;
    Rigidbody2D playerRigidbody;
    public bool IsOnClickFire;
    public float AttackDuration;

    [SerializeField] GameObject arrowObject; //移動方向オブジェクト（矢印オブジェクト）
    SpriteRenderer arrowRenderer;

    [SerializeField] protected ParticleSystem footSmoke;
    [SerializeField] protected float smokePlayVelosity;
    [SerializeField] protected SortingGroup playerSortingGroup;
    Renderer renderer;

    Animator specialCutAnimator;

    //最小の移動量を判定もしくはデフォルトプレイヤー方向に向くように調整

    void Start()
    {
        base.Start();
        specialCutAnimator = GameObject.FindGameObjectWithTag("SpecialCut").GetComponent<Animator>();
        arrowRenderer = arrowObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        arrowObject.SetActive(false);
        playerRigidbody = GetComponent<Rigidbody2D>();
        renderer = footSmoke.GetComponent<Renderer>();
        footSmoke.Stop();
        thisBody = GameObject.FindGameObjectWithTag("PlayerCharaBody");
    }


    void Update()
    {
        if (IsPause()) return;

        NextBehaviorDetermining();

        if (IsNextBehaviorAttack)
        {
            AttackAction();
        }
        else if (IsNextBehaviorMove)
        {
            MoveAction();
        }

        if (Input.GetKey(KeyCode.Z))
        {
            SpecialMoveButton();
        }
        if (Input.GetKey(KeyCode.X))
        {
            SkillButton();
        }

        AddMoveVelosity();

        SmokeEffect();
    }

    /// <summary>
    /// スワイプを行ったとき次行う行動を決定する
    /// スワイプ方向で一定距離以内に敵がいなかった場合は移動、そうでない場合は攻撃を行う
    /// </summary>
    public virtual void NextBehaviorDetermining()
    {
        if (status.IsMovable)
        {
            specialAttackTime += Time.deltaTime;
            if (specialAttackTime > 20f && specialAttackFlg)
            {
                specialAttackFlg = false;
                status.GoToSpecialOff();
            }

            if (IsOnClickSkillButton)
            {
                if (status.IsNormalOfMotionState)
                {
                    IsOnClickSkillButton = false;
                }
                //スキルボタンを押したときは移動方向オブジェクトを出さないようにするためリターンする
                return;
            }

            if (IsOnClickSpecialButton)
            {
                IsOnClickSpecialButton = false;
                //必殺ボタンを押した際は移動方向オブジェクトを出さないようにするためリターンする
                return;
            }

            //プレイヤー移動
            //スワイプした方向にスライド移動する
            GetSwipVelisity();
#if UNITY_WEBGL
            GetArrowVelosity();
#endif

#if UNITY_EDITOR
            GetArrowVelosity();
#endif

            NextMoveON();
        }
    }

    //==============================================================================================
    //次行動決定メソッド
    //==============================================================================================

    /// <summary>
    /// タップ時の移動方向ベロシティ取得メソッド
    /// </summary>
    void GetSwipVelisity()
    {
        //タップ時の座標を取得
        if (Input.GetMouseButtonDown(0))
        {
            startPosition = Input.mousePosition;
            buttonDownFlg = true;
        }
        //タップし続けている間の座標を絶えず取得
        else if (Input.GetMouseButton(0) && buttonDownFlg)
        {
            Vector2 stayPosition = Input.mousePosition;

            if (IsSwipOn(startPosition, stayPosition)) return;

            arrowObject.SetActive(true);
            var rayMoveVelosity = ReturnMoveVelosity(startPosition, stayPosition);
            arrowHit = Physics2DExtentsion.RaycastAndDraw(layOriginPosition.transform.position, rayMoveVelosity, maxDistance, layMask);
            enemyTransform = arrowHit.transform;

            // 矢印オブジェクトをスワイプ方向に回転させる
            var arrowAngle = Mathf.Atan2(rayMoveVelosity.y, rayMoveVelosity.x) * Mathf.Rad2Deg;

            //移動方向の敵取得
            arrowObject.transform.rotation = Quaternion.Euler(0, 0, arrowAngle);

            //移動方向において一定距離内、一定角度内に敵がいた場合は矢印オブジェクトを赤くする
            if (arrowHit.collider)
            {
                float dis = Vector2.Distance(this.transform.position, enemyTransform.position);
                ArrowObjectColorChange(dis, arrowAngle);
            }
            else
            {
                arrowRenderer.color = new Color(1, 1, 1, 1);
            }

            buttonUpFlg = true;
        }
        //タップを離したときの座標を取得
        else if (Input.GetMouseButtonUp(0) && buttonUpFlg)
        {
            arrowObject.SetActive(false);
            buttonDownFlg = false;
            buttonUpFlg = false;
            SwipFlg = true;
            finishPosition = Input.mousePosition;
        }
    }

    /// <summary>
    /// 十字キー操作の移動方向ベロシティ取得メソッド
    /// </summary>
    void GetArrowVelosity()
    {
        if (IsArrowKeyOn() && !buttonDownFlg)
        {
            startPosition = transform.position;
            buttonDownFlg = true;
        }
        if (IsArrowKeyDown() && buttonDownFlg)
        {
            var pos = transform.position;

            float xVelosity = Input.GetAxisRaw("Horizontal");
            float yVelosity = Input.GetAxisRaw("Vertical");

            Vector2 stayPosition = new Vector2(pos.x + xVelosity, pos.y + yVelosity);

            arrowObject.SetActive(true);
            var rayMoveVelosity = ReturnMoveVelosity(startPosition, stayPosition);
            arrowHit = Physics2DExtentsion.RaycastAndDraw(layOriginPosition.transform.position, rayMoveVelosity, maxDistance, layMask);
            enemyTransform = arrowHit.transform;

            // 矢印オブジェクトをスワイプ方向に回転させる
            var arrowAngle = Mathf.Atan2(rayMoveVelosity.y, rayMoveVelosity.x) * Mathf.Rad2Deg;
            arrowObject.transform.rotation = Quaternion.Euler(0, 0, arrowAngle);

            if (arrowHit.collider)
            {
                float dis = Vector2.Distance(this.transform.position, enemyTransform.position);
                ArrowObjectColorChange(dis, arrowAngle);
            }
            else
            {
                arrowRenderer.color = new Color(1, 1, 1, 1);
            }

            finishPosition = stayPosition;
            buttonUpFlg = true;
        }
        if (IsArrowKeyUp() && buttonUpFlg)
        {
            arrowObject.SetActive(false);

            buttonDownFlg = false;
            buttonUpFlg = false;

            SwipFlg = true;
        }
    }

    //移動方向を示す矢印スプライトの色を変更する
    //一定距離内に敵がいたら赤色に変更し攻撃モーションが実行されることを示す
    void ArrowObjectColorChange(float dis, float arrowAngle)
    {
        if (dis > attackMinDistance)
        {
            if (faceAttackableMaxAngle > arrowAngle && arrowAngle > faceAttackableMinAngle)
            {
                arrowRenderer.color = new Color(1, 0.5f, 0.5f, 1);
            }
            else if (backAttackableMaxAngle < arrowAngle || arrowAngle < backAttackableMinAngle)
            {
                arrowRenderer.color = new Color(1, 0.5f, 0.5f, 1);
            }
            else
            {
                arrowRenderer.color = new Color(1, 1, 1, 1);
            }
        }
    }

    //十字キーを押したかどうか判定メソッド　もっといいやり方がありそうなので要検討
    bool IsArrowKeyOn()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //十字キーを押し続けているかどうか判定メソッド　もっといいやり方がありそうなので要検討
    bool IsArrowKeyDown()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //十字キーを離したかどうかの判定メソッド　もっといいやり方がありそうなので要検討
    bool IsArrowKeyUp()
    {
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// タップや十字キー操作で取得したベロシティから次行動を決定する
    /// </summary>
    public virtual void NextMoveON()
    {
        if (SwipFlg)
            {
                tmpEnemyTransform = null;

                raycastVelosity = tmpMoveVelosity = ReturnMoveVelosity(startPosition, finishPosition);
                hit = Physics2DExtentsion.RaycastAndDraw(layOriginPosition.transform.position, raycastVelosity, maxDistance, layMask);
                enemyTransform = hit.transform;

                //プレイヤーの向き変更
                PlayerFaceChenge(tmpMoveVelosity);

                float angle = Mathf.Atan2(raycastVelosity.y, raycastVelosity.x) * Mathf.Rad2Deg;

                //移動方向の一定距離内に敵がいたら攻撃フラグをオンにする
                if (hit.collider && !isMoveOnly)
                {
                    Debug.Log("hit " + hit);
                    float dis = Vector2.Distance(this.transform.position, enemyTransform.position);
                    if (faceAttackableMaxAngle > angle && angle > faceAttackableMinAngle && dis > attackMinDistance)
                    {
                        nextBehavior = NextBehavior.Attack;
                        tmpEnemyTransform = hit.transform;
                    }
                    else if ((backAttackableMaxAngle < angle || angle < backAttackableMinAngle) && dis > attackMinDistance)
                    {
                        nextBehavior = NextBehavior.Attack;
                        tmpEnemyTransform = hit.transform;
                    }
                    else
                    {
                        //攻撃フラグがオンになっていなかったら移動フラグをオンにする
                        nextBehavior = NextBehavior.Move;
                    }
                }
                else
                {
                    //攻撃フラグがオンになっていなかったら移動フラグをオンにする
                    nextBehavior = NextBehavior.Move;
                }

                SwipFlg = false;
            }
    }

    /// <summary>
    /// プレイヤーのベロシティを操作
    /// </summary>
    public virtual void AddMoveVelosity()
    {
        //敵との距離が一定以内になったらベロシティ0に
        if (tmpEnemyTransform != null)
        {
            float distance = Vector2.Distance(tmpEnemyTransform.position, layOriginPosition.transform.position);
            Debug.Log("distance: " + distance);
            if (distance < 0.7f)
            {
                moveVelosity = new Vector2(0, 0);
            }
        }

        playerRigidbody.velocity = moveVelosity;
        //ベロシティを減算する
        moveVelosity.y *= Deceleration;
        moveVelosity.x *= Deceleration;
    }

    /// <summary>
    /// 攻撃メソッド
    /// 攻撃時敵方向へ向きを変更しダメージアニメーションを再生する
    /// </summary>
    public virtual void AttackAction()
    {
        MobStatus TargetStatus = hit.collider.GetComponentInChildren<MobStatus>();
        if (status.IsNormalOfMotionState && AttackHitJudge(TargetStatus, status, AttackAbleDistance))
        {
            //プレイヤーの向き変更
            PlayerFaceChenge(tmpMoveVelosity);
            if (specialAttackTime < 20f)
            {
                playerAnimator.SetTrigger("SpecialAttack");
            }
            else
            {
                playerAnimator.SetTrigger("Attack");
            }
            nextBehavior = NextBehavior.Normal;
            moveVelosity = tmpMoveVelosity;
        }
    }

    /// <summary>
    /// 移動メソッド
    /// </summary>
    public virtual void MoveAction()
    {
        if (status.IsNormalOfMotionState)
        {
            //移動中に移動メソッドが実行された際には移動前のアニメーションステートの"Idle"に移行するようにし
            //すぐさま移動モーションが繰り返し再生されるようにする
            if (!playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                playerAnimator.SetTrigger("Idle");
            }
            playerAnimator.SetTrigger("Move");
            nextBehavior = NextBehavior.Normal;
            moveVelosity = tmpMoveVelosity;
        }
    }

    /// <summary>
    /// 一定以上のベロシティで移動している間は土煙パーティクルをオンにする
    /// </summary>
    public virtual void SmokeEffect()
    {
        if (playerRigidbody.velocity.magnitude > smokePlayVelosity)
        {
            // 再生
            if (!footSmoke.isEmitting)
            {
                renderer.sortingOrder = playerSortingGroup.sortingOrder - 1;
                footSmoke.Play();
            }
        }
        else
        {
            // 停止
            if (footSmoke.isEmitting)
            {
                footSmoke.Stop();
            }
        }
    }
    
    public bool ReturnBool()
    {
        return IsOnClickSkillButton;
    }


    protected override bool AttackHitJudge(MobStatus targetStatus, MobStatus myStatus, float attackAbleDistance)
    {
        return base.AttackHitJudge(targetStatus, myStatus, attackAbleDistance);
    }


    public void PlayerFaceChenge(Vector2 velosity)
    {
        if (velosity.x != 0 && status.IsNormalOfMotionState)
        {
            if (velosity.x > 0)
            {
                thisBody.transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                thisBody.transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }
    }

    /// <summary>
    /// 一定以上スワイプしたか判定
    /// 一定距離以上スワイプしないと移動方向矢印を表示させない
    /// </summary>
    /// <param name="startPosition"></param>
    /// <param name="stayPosition"></param>
    /// <returns></returns>
    bool IsSwipOn(Vector2 startPosition, Vector2 stayPosition)
    {
        var swipDistance = startPosition - stayPosition;
        if(Mathf.Abs(swipDistance.x) > SWIP_ABLE_DISTANCE && Mathf.Abs(swipDistance.y) > SWIP_ABLE_DISTANCE)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    //=============================================================================================
    //スワイプ、十字キー操作時のベロシティ・角度取得メソッド
    //=============================================================================================

    /// <summary>
    /// タップ、十字キー操作から取得したスタートポジション、フィニッシュポジションからベロシティを取得する
    /// </summary>
    /// <param name="startPosition"></param>
    /// <param name="finishPosition"></param>
    /// <returns></returns>
    public Vector2 ReturnMoveVelosity(Vector2 startPosition, Vector2 finishPosition)
    {
        float x = (finishPosition.x - startPosition.x);
        float y = (finishPosition.y - startPosition.y);
        var tmpVelosity = new Vector2(x, y).normalized;
        tmpVelosity.x *= xMoveSpeed;
        tmpVelosity.y *= yMoveSpeed;

        return tmpVelosity;
    }

    /// <summary>
    /// タップ、十字キー操作時の矢印オブジェクトの角度を返す
    /// </summary>
    /// <param name="startPosition"></param>
    /// <param name="finishPosition"></param>
    /// <returns></returns>
    public float ReturnAngle(Vector2 startPosition, Vector2 finishPosition)
    {
        Vector2 forward = startPosition - finishPosition;
        forward *= -1;
        float angle = Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg;
        return angle;
    }

    //=============================================================================================
    //攻撃ヒット判定メソッド
    //=============================================================================================

    public void OnCounterAttack(Collider2D collider)
    {
        //Debug.Log("カウンター" + collider);
        var targetObjPosition = collider.transform.root.gameObject.transform.position;
        var ownObjPosition = transform.position;

        float x = (targetObjPosition.x - ownObjPosition.x);
        float y = (targetObjPosition.y - ownObjPosition.y);
        var tmpMoveVelosity = new Vector2(x, y).normalized;

        if (tmpMoveVelosity.x > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        playerAnimator.SetTrigger("CounterAttack");
    }

    public void OnHitAttack(Collider2D collider)
    {
        Debug.Log("OnHitAttackStart");
        var targetMob = collider.GetComponentInChildren<MobStatus>();
        //Debug.Log(targetMob);
        if (null == targetMob) return;
        if (Mathf.Abs(targetMob.shadow.ReturnYPosition() - status.ReturnShadowPosition()) > 1)
        {
            return;
        }

        Debug.Log("OnHitAttackUp");
        targetMob.Damage(1);
    }

    //=============================================================================================
    //ボタン用メソッド
    //=============================================================================================
    public virtual void IsMoveOnlyButton()
    {
        if (isMoveOnly)
        {
            isMoveOnly = false;
        }
        else
        {
            isMoveOnly = true;
        }
    }

    public virtual void SpecialMoveButton()
    {
        if (status.IsSpecialMoveStateOn && status.IsMovable && !specialAttackFlg)
        {
            IsOnClickSpecialButton = true;
            specialAttackTime = 0;
            specialAttackFlg = true;
            if(specialCutAnimator) specialCutAnimator.SetTrigger("CutOn");
        }
    }

    public virtual void SkillButton()
    {
        if (status.IsNormalOfMotionState)
        {
            playerAnimator.SetTrigger("CounterWait");
            IsOnClickSkillButton = true;
        }
    }

}

//分けといたほうが良い？
/// <summary>
/// Physics2Dの拡張クラス　
/// </summary>
public static class Physics2DExtentsion
{

    //Rayの表示時間
     const float RAYDISPLAYTIME = 3;

    /// <summary>
    /// Rayを飛ばすと同時に画面に線を描画する
    /// </summary>
    public static RaycastHit2D RaycastAndDraw(Vector2 origin, Vector2 direction, float maxDistance, int layerMask)
    {
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, maxDistance, layerMask);

        //衝突時のRayを画面に表示
        if (hit.collider)
        {
            Debug.DrawRay(origin, hit.point - origin, Color.blue, RAYDISPLAYTIME, false);
        }
        //非衝突時のRayを画面に表示
        else
        {
            Debug.DrawRay(origin, direction * maxDistance, Color.green, RAYDISPLAYTIME, false);
        }

        return hit;
    }

}

