using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//çÏê¨ìríÜ
public class GunAnimationEvent : MonoBehaviour
{
    [SerializeField] private AhyaKnifeStatus ahyaKnifeStatus;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttaackColliderOn()
    {
        ahyaKnifeStatus.AttackColliderOn();
    }

    public void AttackColliderOff()
    {
        ahyaKnifeStatus.AttackColliderOff();
    }
}
