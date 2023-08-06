using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// combo加算制御クラス
/// </summary>
public class ComboController : MonoBehaviour
{
    [SerializeField] private List<MobStatus> enemyStatuses;
    ComboUISystem comboUISystem;

    // Start is called before the first frame update
    void Start()
    {
        comboUISystem = GetComponent<ComboUISystem>();
    }

    /// <summary>
    /// 敵キャラクターの被ダメージ時呼ばれコンボを加算する
    /// </summary>
    public void comboPlus()
    {
        comboUISystem.IncreaseCombo();
    }

    public void addEnemyStatus(MobStatus mobstatuts)
    {
        enemyStatuses.Add(mobstatuts);
    }
}
