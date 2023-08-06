using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// キャラクターのステータスを時間指定で変更するクラス
/// ステータス変更は現在アニメーションイベントで行っている
/// デバッグが面倒なためスクリプト上で制御したい
/// 作成途中
/// </summary>
public class StatusControll : MonoBehaviour
{
    private MobStatus status;

    private float motionTime;
    private float motionProgressTime = 0;
    private bool defenseStartFlg = false;
    private float defenseTime;
    private float defenseStartTime = 0;
    private float defenseProgressTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        status = GetComponent<MobStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        if (status.IsMotionOfMotionState)
        {
            motionProgressTime += Time.deltaTime;
            if (motionTime < motionProgressTime)
            {
                status.SetMotionStateToNormal();
                motionProgressTime = 0;
            }
        }

        if (defenseStartFlg)
        {
            defenseProgressTime += Time.deltaTime;

            if (defenseProgressTime > defenseStartTime)
            {
                status.SetGuardStateToDefense();
            }

            if (defenseTime < defenseProgressTime)
            {
                status.SetGuardStateToNormal();
                defenseProgressTime = 0;
                defenseStartFlg = false;
            }
        }
    }

    public void StartMotionOfMotionState(float cnfTime)
    {
        motionTime = cnfTime;
        motionProgressTime = 0;
        status.SetMotionStateToMotion();
    }

    public void StartDefenseOfGuardState(float cnfTime, float cnfStartTime = 0)
    {
        defenseTime = cnfTime;
        defenseStartTime = cnfStartTime;
        defenseProgressTime = 0;
        defenseStartFlg = true;

    }
}