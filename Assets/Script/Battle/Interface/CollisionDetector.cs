using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// “–‚½‚è”»’è‹¤’ÊƒNƒ‰ƒX
/// </summary>
//[RequireComponent(typeof(Collider))]
public class CollisionDetector : MonoBehaviour
{

    [SerializeField] private TriggerEvent onTriggerEnter = new TriggerEvent();
    [SerializeField] private TriggerEvent onTriggerStay = new TriggerEvent();

    private void OnTriggerEnter2D(Collider2D other)
    {
        string objName = other.gameObject.name;
        //Debug.Log("OnTriggerEnter ObjectName: " + objName);
        onTriggerEnter.Invoke(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        string objName = other.gameObject.name;
        //Debug.Log("OnTriggerStay ObjectName: " + objName);
        onTriggerStay.Invoke(other);
    }

    [Serializable]
    public class TriggerEvent : UnityEvent<Collider2D>
    {

    }
}
