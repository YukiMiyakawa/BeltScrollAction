using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// AhyaKnifeのステータス制御クラス
/// </summary>
public class AhyaKnifeStatus : EnemyStatus
{
    void Start()
    {
        base.Start();
        //comboController = GameObject.Find("GameManager").transform.Find("ComboController").GetComponent<ComboController>();
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
}
