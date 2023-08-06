using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// combo���Z����N���X
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
    /// �G�L�����N�^�[�̔�_���[�W���Ă΂�R���{�����Z����
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
