using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// キャラクターの影管理クラス
/// </summary>
public class Shadow : MonoBehaviour
{
    private Transform transform;
    private float zPosition;
    [SerializeField] private GameObject body;
    [SerializeField] private MobStatus mobStatus;

    private void Start()
    {
        transform = GetComponent<Transform>();
    }

    public float ReturnZPosition()
    {
        return transform.position.z;
    }

    public float ReturnYPosition()
    {
        return transform.position.y;
    }

    /// <summary>
    /// 範囲攻撃のダメージを受ける際はキャラクターの影が範囲内にあるかどうかであたり判定を行う。
    /// 本クラスがアタッチされた影スプライトを感知したこのメソッドを呼び出しダメージを与える。
    /// </summary>
    /// <param name="i">ダメージ数値</param>
    public void Damege(int i)
    {
        mobStatus.Damage(i);
    }
}
