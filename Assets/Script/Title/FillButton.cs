using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
//削除予定
public class FillButton : MonoBehaviour
{
    [SerializeField] private Image fillImage;

    private const float DURATION = 0.3f;

    private void Awake()
    {
        fillImage.fillAmount = 0;
    }

    public void OnClick()
    {
        //    fillImage.DOFillAmount(1, DURATION)
        //        .OnComplete(() =>
        //        {
        //            // ボタン押下時の処理
        //            Debug.Log("押された&アニメーション終わった！");
        //        });
    }
}