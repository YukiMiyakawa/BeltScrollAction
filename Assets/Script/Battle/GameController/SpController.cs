using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// SPバー制御クラス
/// </summary>
public class SpController : MonoBehaviour
{
    [SerializeField] ComboUISystem comboUISystem;
    [SerializeField] Image spBarFill;

    // Start is called before the first frame update
    void Start()
    {
        comboUISystem = GameObject.FindGameObjectWithTag("GameManager").GetComponentInChildren<ComboUISystem>();
        spBarFill.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSpGauge();
    }

    public void UpdateSpGauge() {
        spBarFill.fillAmount = (float)comboUISystem.GetSpGauge() / (float)comboUISystem.GetMaxSpGauge();
    }
}
