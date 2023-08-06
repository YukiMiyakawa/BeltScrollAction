using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カメラ制御クラス
/// </summary>
public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] protected float maxXPosition = 2.5f;
    [SerializeField] protected float minXPosition = -2.5f;
    [SerializeField] protected GameObject stage;

    protected GameObject playerObj;
    protected PlayerController player;
    protected Transform playerTransform;

    protected virtual void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        playerTransform = playerObj.transform;
        //SetMaxMinPosition(stage);
    }
    protected virtual void LateUpdate()
    {
        MoveCamera();
    }

    protected void MoveCamera()
    {
        var cameraPos = transform.position;
        var xMoveDistance = Mathf.Lerp(cameraPos.x, playerTransform.position.x, 3.0f * Time.deltaTime);
        cameraPos.x = xMoveDistance;

        cameraPos.x = Mathf.Clamp(cameraPos.x, minXPosition, maxXPosition);

        transform.position = cameraPos;
        //横方向だけ追従
    }

    protected void SetMaxMinPosition(GameObject stage)
    {
        if (!stage) return;

        var stageWidth = stage.GetComponent<SpriteRenderer>().bounds.size.x;
        var screenWidth = Screen.width;
        if(stageWidth > screenWidth)
        {
            var cameraMoveDistance = (stageWidth - screenWidth) / 2;
            maxXPosition = cameraMoveDistance;
            minXPosition = -cameraMoveDistance;
        }
    }
}
