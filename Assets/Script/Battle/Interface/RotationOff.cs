using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��]����N���X
/// </summary>
public class RotationOff : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
