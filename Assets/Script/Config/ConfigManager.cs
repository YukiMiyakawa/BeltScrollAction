using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigManager : MonoBehaviour
{
    //���ʐݒ�
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
    //BGM�ESE���ʐݒ�
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
    //�ݒ蔽�f
    //=============================================================================================
    /// <summary>
    /// �ݒ蔽�f���\�b�h
    /// </summary>
    public void SaveConfig()
    {
        SaveVolume();
    }

    /// <summary>
    /// �ۑ��������V�[���ֈړ�����ۂ͐ݒ�𖢔��f�Ƃ���
    /// </summary>
    void SceneLoSceneLoaded()
    {
        audioManager.ChangeVolume(bgm, se);
    }

}
