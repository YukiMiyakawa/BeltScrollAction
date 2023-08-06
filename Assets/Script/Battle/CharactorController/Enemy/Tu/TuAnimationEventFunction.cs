using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//çÏê¨ìríÜ
public class TuAnimationEventFunction : MonoBehaviour
{
    [SerializeField] TuController tuController;
    [SerializeField] Animator rainKnifeThrowUpAnimator;
    [SerializeField] GameObject rainKnifeDownSet;

    public void FunctionBehaviorFlgOn()
    {
        tuController.BehaviorFlgOn();
    }

    public void FunctionWaitSet()
    {
        //tuController.SetWaitTime(3f, "UpMove");
    }

    public void FunctionStepMove()
    {
        tuController.StepAttackMove();
    }

    public void FunctionTriggerReset()
    {
        tuController.TriggerReset();
    }

    public void FunctionPositionChange()
    {
        tuController.PositionChange();
    }

    public void FunctionKnifeThrow()
    {
        tuController.KnifeThrow();
    }

    public void FunctionRainKnifeThrowUp()
    {
        rainKnifeThrowUpAnimator.SetTrigger("Up");
        tuController.RainKnifeDownPositionChange();
        rainKnifeDownSet.SetActive(true);
    }
}
