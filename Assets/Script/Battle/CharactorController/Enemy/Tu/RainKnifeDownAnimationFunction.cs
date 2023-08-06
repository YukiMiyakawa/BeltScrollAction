using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//çÏê¨ìríÜ
public class RainKnifeDownAnimationFunction : MonoBehaviour
{
    [SerializeField] GameObject rainKnifeDownSet;
    [SerializeField] TuStatus tuStatus;

    public void RainKnifeDownSetActiveOff()
    {
        rainKnifeDownSet.SetActive(false);
    }

    public void FunctionHitOn()
    {
        tuStatus.RainKnifeAttackColliderOn();
    }

    public void FunctionHitOff()
    {
        tuStatus.RainKnifeAttackColliderOff();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
