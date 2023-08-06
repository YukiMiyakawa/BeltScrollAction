using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigManager : MonoBehaviour
{
    //音量設定
    AudioManager audioManager;
    float bgm, se;
    [SerializeField] Slider bgmSlider, seSlider;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        bgm = PlayerPrefs.GetFloat("BGM_VOLUME_KEY", audioManager.GetDefultBGMVolume());
        se = PlayerPrefs.GetFloat("SE_VOLUME_KEY", audioManager.GetDefultSEVolume());
        bgmSlider.value = bgm;
        seSlider.value = se;
    }

    // Update is called once per frame
    void Update()
    {
        if(audioManager.AttachBGMSource.volume != bgmSlider.value ||
            audioManager.AttachSESource.volume != seSlider.value)
        {
            tmpBgmAndSeChange();
        }
    }

    //=============================================================================================
    //BGM・SE音量設定
    //=============================================================================================
    void tmpBgmAndSeChange()
    {
        audioManager.ChangeVolume(bgmSlider.value, seSlider.value);
    }

    void SaveVolume()
    {
        audioManager.SaveChangeVolume(bgmSlider.value, seSlider.value);
        bgm = bgmSlider.value;
        se = seSlider.value;
    }


    //=============================================================================================
    //設定反映
    //=============================================================================================
    /// <summary>
    /// 設定反映メソッド
    /// </summary>
    public void SaveConfig()
    {
        SaveVolume();
    }

    /// <summary>
    /// 保存せず他シーンへ移動する際は設定を未反映とする
    /// </summary>
    void SceneLoSceneLoaded()
    {
        audioManager.ChangeVolume(bgm, se);
    }

}
