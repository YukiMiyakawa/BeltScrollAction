using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// フェードアウト、ポップアップ表示時に使用するフェードアウトスクリーンのサイズ調整クラス
/// </summary>
public class FadeScreenSizeController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //FillScreen();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FillScreen()
    {
        var sr = GetComponent<RectTransform>();

        // カメラの外枠のスケールをワールド座標系で取得
        float worldScreenHeight = Camera.main.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        //  両者の比率を出してスプライトのローカル座標系に反映
        sr.sizeDelta = new Vector2(worldScreenWidth, worldScreenHeight);

        // カメラの中心とスプライトの中心を合わせる
        Vector3 camPos = Camera.main.transform.position;
        camPos.z = 0;
        transform.position = camPos;
    }
}
