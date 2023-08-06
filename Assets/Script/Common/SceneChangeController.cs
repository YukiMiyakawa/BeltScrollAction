using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// シーンチェンジ制御クラス
/// DOTweenを使用したクラスを別途作成したため削除予定
/// </summary>
public class SceneChangeController : MonoBehaviour
{
    [SerializeField] Image fadeScreen;
    Image instantFadeScreen;
    string sceneName;
    public string SceneName { get; set; }

    const float FADE_SPEED = 0.1f;
    float alfa;

    bool isFadeOut;
    bool isFadeIn;

    Transform canvasTransform;

    void Start()
    {
        canvasTransform = GameObject.Find("Canvas").transform;
        instantFadeScreen = Instantiate(fadeScreen, canvasTransform);

        RectTransform imageRectTransform = instantFadeScreen.GetComponent<RectTransform>();
        float x = Screen.width;
        float y = Screen.height;

        imageRectTransform.sizeDelta = new Vector2(x, y);
        alfa = 1;
        SetAlpha();
        isFadeOut = true;
    }

    private void Update()
    {
        if (isFadeOut)
        {
            FadeOut();
        }
        if (isFadeIn)
        {
            FadeIn();
        }
    }

    public void SceneChange(string sceneName = "")
    {
        if (sceneName != null)
        {
            this.sceneName = sceneName;
        }
        else
        {
            Debug.LogError("sceneNameが指定されていません");
        }
        instantFadeScreen.enabled = true;
        isFadeIn = true;
    }

    void FadeOut()
    {
        if(alfa > 0)
        {
            alfa -= FADE_SPEED;
            SetAlpha();
        }
        else
        {
            isFadeOut = false;
            instantFadeScreen.enabled = false;
        }
    }

    void FadeIn()
    {
        if(alfa < 1)
        {
            alfa += FADE_SPEED;
            SetAlpha();
        }
        else
        {
            isFadeIn = false;
            SceneManager.LoadScene($"{sceneName}");
        }
    }

    void SetAlpha()
    {
        instantFadeScreen.color = new Color(0,0,0,alfa);
    }

    public void Retry()
    {
        sceneName = SceneManager.GetActiveScene().name;
        SceneChange();
    }
}
