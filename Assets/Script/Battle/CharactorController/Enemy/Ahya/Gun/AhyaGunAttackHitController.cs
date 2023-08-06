using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// çUåÇîªíËÉNÉâÉX
/// </summary>
public class AhyaGunAttackHitController : MonoBehaviour
{
    [SerializeField] private AhyaGunController ahyaGunController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("AhyaGunAttackHit");
        ahyaGunController.OnHitAttack(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("AhyaGunAttackStay");
    }

}
