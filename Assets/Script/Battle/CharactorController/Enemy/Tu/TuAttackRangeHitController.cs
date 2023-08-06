using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuAttackRangeHitController : MonoBehaviour
{
    [SerializeField] TuController tuController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("AhyaGunAttackHit");
        tuController.OnHitRangeAttack(collision);

    }
}
