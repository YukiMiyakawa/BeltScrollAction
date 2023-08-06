using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MobStatus
{
    /// <summary>
    /// 敵キャラクターステータス管理クラス
    /// </summary>
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        //敵キャラクターがダメージを受けた際コンボコントローラーが感知してコンボ数を計上する
        comboController = GameObject.Find("GameManager").transform.Find("ComboController").GetComponent<ComboController>();
        comboController.addEnemyStatus(this.GetComponent<MobStatus>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
