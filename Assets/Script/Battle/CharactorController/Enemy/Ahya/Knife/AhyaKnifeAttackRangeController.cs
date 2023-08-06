using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// çUåÇîªíËÉNÉâÉX
/// </summary>
public class AhyaKnifeAttackRangeController : MonoBehaviour
{
    [SerializeField] AhyaKnifeController ahyaKnifeController;

    float stayTimer = 0;
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
        stayTimer = 3;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log("AhyaStayStart");
        if (other.gameObject.tag != "Player") return;
        //Debug.Log("AhyaStayOn");
        stayTimer -= Time.deltaTime;

        if(stayTimer < 0 && null != ahyaKnifeController)
        {
            ahyaKnifeController.Attack();
        }
    }
}
