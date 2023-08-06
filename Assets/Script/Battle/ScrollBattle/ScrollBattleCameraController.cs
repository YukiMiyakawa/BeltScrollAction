using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 無限スクロールステージのカメラ制御クラス
/// ステージデザインの方を工夫して通常ステージのカメラ制御クラスを併用できるように検討中
/// </summary>
public class ScrollBattleCameraController : MonoBehaviour
{
    [SerializeField] StageScrollController stageScrollController;
    private float MAX_X_POSITION = 1.9f;
    private float MIN_X_POSITION = -1.9f;

    GameObject playerObj;
    PlayerController player;
    Transform playerTransform;

    float cameraSpeed = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        playerTransform = playerObj.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        MoveCamera();
    }

    void MoveCamera()
    {
        var cameraPos = transform.position;
        var xMoveDistance = Mathf.Lerp(cameraPos.x, playerTransform.position.x, 3.0f * Time.deltaTime);
        cameraPos.x = xMoveDistance;

        cameraPos.x = Mathf.Clamp(cameraPos.x, GetMinPosition(), GetMaxPosition());

        transform.position = cameraPos;
    }

    float GetMaxPosition()
    {
        return stageScrollController.GetThirdStagePositionX() + MAX_X_POSITION;
    }

    float GetMinPosition()
    {
        return stageScrollController.GetFirstStagePositionX() + MIN_X_POSITION;
    }
}
