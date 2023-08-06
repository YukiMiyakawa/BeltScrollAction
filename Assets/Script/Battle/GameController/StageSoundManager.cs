using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BGM制御クラス
/// 現状使用されていないので削除予定
/// </summary>
public class StageSoundManager : MonoBehaviour
{
    //ボリューム保存用のkeyとデフォルト値
    private const string BGM_VOLUME_KEY = "BGM_VOLUME_KEY";
    private const float BGM_VOLUME_DEFULT = 0.5f;

    //BGMがフェードするのにかかる時間
    public const float BGM_FADE_SPEED_RATE_HIGH = 0.9f;
    public const float BGM_FADE_SPEED_RATE_LOW = 0.3f;
    private float bgmFadeSpeedRate = BGM_FADE_SPEED_RATE_HIGH;

    //初めに流すBGM
    public string firstBgmName = "Battle1";

    //次流すBGM名
    private string nextBGMName;

    //BGMをフェードアウト中か
    private bool isFadeOut = false;

    //BGM用オーディオソース
    public AudioSource AttachBGMSource;

    //全Audioを保持
    Dictionary<string, AudioClip> bgmDic;


    // Use this for initialization
    private void Start()
    {
        bgmDic = new Dictionary<string, AudioClip>();
        object[] bgmList = Resources.LoadAll("Audio/BGM");

        foreach (AudioClip bgm in bgmList)
        {
            bgmDic[bgm.name] = bgm;
        }

        AttachBGMSource.volume = PlayerPrefs.GetFloat(BGM_VOLUME_KEY, BGM_VOLUME_DEFULT);

        AttachBGMSource.clip = bgmDic[firstBgmName] as AudioClip;
        AudioListener.volume = BGM_VOLUME_DEFULT;
        AttachBGMSource.Play();
    }

    private void Update()
    {
        if (!isFadeOut)
        {
            return;
        }

        //徐々にボリュームを下げていき、ボリュームが0になったらボリュームを戻し次の曲を流す
        AttachBGMSource.volume -= Time.deltaTime * bgmFadeSpeedRate;
        if (AttachBGMSource.volume <= 0)
        {
            AttachBGMSource.Stop();
            //AttachBGMSource.volume = PlayerPrefs.GetFloat(BGM_VOLUME_KEY, BGM_VOLUME_DEFULT);
            isFadeOut = false;

            if (!string.IsNullOrEmpty(nextBGMName))
            {
                AttachBGMSource.clip = bgmDic[nextBGMName] as AudioClip;
                AttachBGMSource.volume = BGM_VOLUME_DEFULT;
                AttachBGMSource.loop = !AttachBGMSource.loop;
                AttachBGMSource.Play();
            }
        }
    }

    public void BgmChange(string nextStageState)
    {
        if(nextStageState != "Victory" && nextStageState != "Defeat")
        {
            Debug.LogError("引数にはVictoryかDefeatを指定してください");
            return;
        }
        else if(nextStageState == "Victory")
        {
            nextBGMName = "Victory";
        }
        else if(nextStageState == "Degeat")
        {
            nextBGMName = "Victory";
        }

        FadeOutBGM();
    }

    public void FadeOutBGM(float fadeSpeedRate = BGM_FADE_SPEED_RATE_LOW)
    {
        bgmFadeSpeedRate = fadeSpeedRate;
        isFadeOut = true;
    }
}
