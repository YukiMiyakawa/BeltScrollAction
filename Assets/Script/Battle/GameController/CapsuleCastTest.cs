using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カプセル状のraycast作成クラス
/// 作成途中
/// </summary>
public class CapsuleCastTest : MonoBehaviour
{
    private CapsuleDirection2D capsuleDirection2D;
    private CapsuleCollider2D capsuleCollider2D;
    private Vector2 direction;
    private RaycastHit2D isHit;
    private GUIStyle style;
    [SerializeField] LayerMask layMask = 7;
    [SerializeField] float angle = 0;
    [SerializeField] private float maxRange = 1f;

    // Start is called before the first frame update
    void Start()
    {
        capsuleCollider2D = this.GetComponent<CapsuleCollider2D>();
        capsuleDirection2D = capsuleCollider2D.direction;
        style = new GUIStyle();
        style.fontSize = 60;
    }

    // Update is called once per frame
    void Update()
    {

        var center = transform.position;
        var tmpForword = transform.rotation.z;
        direction = ForwordToVector2(tmpForword);

        // CapsuleCastによる当たり判定
        isHit = Physics2D.CapsuleCast(
            center, // 始点
            transform.localScale,
            capsuleDirection2D,
            //transform.rotation.z,
            angle,
            direction,
            maxRange,
            layMask
        );

        // 結果表示
    }

    public Vector2 ForwordToVector2(float forword)
    {
        var radian = forword * (Mathf.PI / 180);
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian)).normalized;
    }

    public void OnGUI()
    {
        if (isHit)
        {
            GUI.Box(new Rect(20, 500, 400, 300), $"Hit! - {isHit.collider}", style);
            Debug.Log($"Hit! - {isHit.collider}");
        }
        else
        {
            GUI.Box(new Rect(20, 500, 400, 300), "None", style);
            Debug.Log("None");
        }
        GUI.Box(new Rect(20, 550, 400, 300), direction.ToString() + "rotation: " + transform.rotation.z, style);

    }
}


