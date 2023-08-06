using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// キャラクター動作制御基底クラス
/// </summary>
public class MobConrtoller : MonoBehaviour
{
    protected PauseController pauseController;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        pauseController = GameObject.FindGameObjectWithTag("GameManager").GetComponentInChildren<PauseController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 2Dプロジェクトにおいて奥行を測るために攻撃時お互いの影のYポジションの距離を判定する。
    /// </summary>
    /// <param name="targetStatus">攻撃がヒットした相手のステータス</param>
    /// <param name="myStatus"></param>
    /// <param name="attackAbleDistance"></param>
    /// <returns></returns>
    protected virtual bool AttackHitJudge(MobStatus targetStatus, MobStatus myStatus, float attackAbleDistance)
    {
        float tmpDistance = Mathf.Abs(targetStatus.ReturnShadowPosition() - myStatus.ReturnShadowPosition());
        if (tmpDistance < attackAbleDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 画面ポーズ状態かどうかを判定
    /// </summary>
    /// <returns></returns>
    public bool IsPause()
    {
        if (pauseController != null)
        {
            return pauseController.IsPause();
        }
        else
        {
            Debug.Log("MobPauseFalse");
            return false;
        }
    }
}
