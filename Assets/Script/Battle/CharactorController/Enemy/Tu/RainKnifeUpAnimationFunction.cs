using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//çÏê¨ìríÜ
public class RainKnifeUpAnimationFunction : MonoBehaviour
{
    //[SerializeField] GameObject rainKnifeDownSet;
    [SerializeField] Animator rainKnifeThrowDownAnimator;
    //[SerializeField] TuController tuController;

    public void FunctionRainKnifeThrowDown()
    {
        //tuController.RainKnifeDownPositionChanfe();
        rainKnifeThrowDownAnimator.SetTrigger("Down");
        //rainKnifeDownSet.SetActive(true);
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
