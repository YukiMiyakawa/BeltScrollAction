using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// 敵キャラクター制御クラス
/// </summary>
public class EnemyController : MobConrtoller
{
    //移動スピード
    [SerializeField] protected float xSpeed = 10f;
    [SerializeField] protected float ySpeed = 5f;
    [SerializeField] protected float speed = 2.5f;
    [SerializeField] protected float deceleration = 0.98f;　//ステップ移動時の減速度

    [SerializeField] protected Animator enemyAnimator;
    [SerializeField] protected MobStatus status;

    protected Transform myTransform;
    public Transform playerTransfrom;
    protected Rigidbody2D myRigidbody2D;
    protected GameObject player;
    protected Vector2 velosity;

    Vector2 faceVelosity; //向き方向ベロシティ
    [SerializeField] protected SortingGroup mySortingGroup; 

    //ステップ移動時土煙
    [SerializeField] protected ParticleSystem footSmoke;
    [SerializeField] protected float smokePlayVelosity;
    Renderer smokeRenderer;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransfrom = player.transform;
        myRigidbody2D = GetComponent<Rigidbody2D>();
        status = GetComponentInChildren<MobStatus>();
        if(footSmoke != null)
        {
            smokeRenderer = footSmoke.GetComponent<Renderer>();
            footSmoke.Stop();
        }
    }

    protected virtual void Update()
    {
        faceVelosity = GetVelosityToPlayer();
    }

    /// <summary>
    /// プレイヤー方向を取得
    /// </summary>
    /// <returns>プレイヤー方向のノーマライズされたベロシティ</returns>
    protected Vector2 GetVelosityToPlayer()
    {
        float x = playerTransfrom.position.x - transform.position.x;
        float y = playerTransfrom.position.y - transform.position.y;

        return new Vector2(x, y).normalized;
    }

    /// <summary>
    /// ステップ移動メソッド
    /// </summary>
    /// <returns>プレイヤー方向へのベロシティを返す</returns>
    public Vector2 GetStepMoveVelosity()
    {
        float x = playerTransfrom.position.x - transform.position.x;
        float y = playerTransfrom.position.y - transform.position.y;
        var tmpVelosity = new Vector2(x, y).normalized;

        tmpVelosity.x *= xSpeed;
        tmpVelosity.y *= ySpeed;

        return tmpVelosity;
    }

    /// <summary>
    /// シームレス移動メソッド
    /// </summary>
    protected void MoveMethod()
    {
        float x = playerTransfrom.position.x - transform.position.x;
        float y = playerTransfrom.position.y - transform.position.y;

        if ((x > 1.0f || x < -1.0f) || (y > 0.5f || y < -0.5f))
        {
            velosity = new Vector2(x * xSpeed, y * ySpeed).normalized * speed;
            myRigidbody2D.velocity = velosity;
            enemyAnimator.SetFloat("MoveSpeed", velosity.magnitude);
        }
        else
        {
            velosity = new Vector2(0, 0);
            myRigidbody2D.velocity = velosity;
            enemyAnimator.SetFloat("MoveSpeed", velosity.magnitude);
        }

        if (x > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    /// <summary>
    /// 向き変更メソッド
    /// </summary>
    /// <param name="velosity">移動方向ベロシティ</param>
    public void FaceChenge(Vector2 velosity)
    {
        if (velosity.x > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    /// <summary>
    /// 現在のプレイヤー方向を取得
    /// </summary>
    /// <returns></returns>
    public Vector2 returnFaceVelosity()
    {
        return faceVelosity;
    }

    /// <summary>
    /// 与ダメージメソッド
    /// </summary>
    /// <param name="collider">与ダメージ相手のCollider</param>
    public virtual void OnHitAttack(Collider2D collider)
    {
        var targetMob = collider.GetComponentInChildren<MobStatus>();
        if (null == targetMob) return;
        if (Mathf.Abs(targetMob.ReturnShadowPosition() - status.ReturnShadowPosition()) > 1)
        {
            return;
        }
        targetMob.Damage(1);
    }

    /// <summary>
    /// ステップ移動時の土煙表示メソッド
    /// </summary>
    public virtual void SmokeEffect()
    {
        if (myRigidbody2D.velocity.magnitude > smokePlayVelosity)
        {
            // 再生
            if (!footSmoke.isEmitting)
            {
                smokeRenderer.sortingOrder = mySortingGroup.sortingOrder - 1;
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




}
