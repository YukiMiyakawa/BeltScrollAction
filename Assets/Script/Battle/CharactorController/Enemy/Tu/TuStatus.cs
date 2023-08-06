using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//作成途中
public class TuStatus : EnemyStatus
{
    Transform playerTransfrom;
    [SerializeField] GameObject Tu;
    [SerializeField] TuController tuController;
    [SerializeField] Collider2D knifeCollider;
    [SerializeField] Collider2D rainKnifeCollider;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        var player = GameObject.FindGameObjectWithTag("Player");
        playerTransfrom = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Damage(int damage)
    {
        Debug.Log(IsDefenseOfGuardState);
        if (state == StateEnum.Die || IsDefenseOfGuardState) return;
        life -= damage;

        //hitパーティクルレイヤー変更
        renderer.sortingOrder = charactorSortingGroup.sortingOrder + 1;
        hitEffect.Play();

        //コンボプラスメソッド
        comboController.comboPlus();

        if (life > 0)
        {
            animator.SetTrigger("WaitState");
            animator.SetTrigger("Damage");
            return;
        }

        state = StateEnum.Die;
        animator.SetTrigger("Die");
        OnDie();
    }

    public void PositionChenge()
    {
        Tu.SetActive(false);
        if(playerTransfrom.position.x < 0)
        {
            tuController.FaceChenge(new Vector2(-1, 0));
            Tu.transform.position.Set(3.7f, 0.4f, 0);
            animator.SetTrigger("DownMove");
            Tu.SetActive(true);
        }
        else
        {
            tuController.FaceChenge(new Vector2(1, 0));
            Tu.transform.position.Set(-3.7f, 0.4f, 0);
            animator.SetTrigger("DownMove");
            Tu.SetActive(true);
        }
    }

    public void knifeAttackColliderOn()
    {
        //Debug.Log("attackOn");
        knifeCollider.enabled = true;
        knifeCollider.isTrigger = true;
    }

    public void knifeAttackColliderOff()
    {
        //Debug.Log("attackOff");
        knifeCollider.enabled = false;
        knifeCollider.isTrigger = false;
    }

    public void RainKnifeAttackColliderOn()
    {
        //Debug.Log("attackOn");
        rainKnifeCollider.enabled = true;
        rainKnifeCollider.isTrigger = true;
    }

    public void RainKnifeAttackColliderOff()
    {
        //Debug.Log("attackOff");
        rainKnifeCollider.enabled = false;
        rainKnifeCollider.isTrigger = false;
    }
}
