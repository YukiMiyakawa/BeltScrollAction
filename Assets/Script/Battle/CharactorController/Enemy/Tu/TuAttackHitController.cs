using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//çÏê¨ìríÜ
public class TuAttackHitController : MonoBehaviour
{
    [SerializeField] TuController tuController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("AhyaGunAttackHit");
        tuController.OnHitAttack(collision);
        
    }
}
