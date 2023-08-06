using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 戦闘時など各キャラがSEを鳴らしたいときに使用する
/// </summary>
public class CharaSeManeger : MonoBehaviour
{
    private const string SE_VOLUME_KEY = "SE_VOLUME_KEY";
    private const float SE_VOLUME_DEFULT = 1.0f;

    public AudioSource seSource;
    private Dictionary<string, AudioClip> seDic;

    // Start is called before the first frame update
    void Start()
    {
        //リソースから全SEを取得
        seDic = new Dictionary<string, AudioClip>();
        object[] seList = Resources.LoadAll("Audio/SE");
        foreach (AudioClip se in seList)
        {
            seDic[se.name] = se;
        }
        seSource.volume = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void SEPlay(string seName)
    {
        if (!seDic.ContainsKey(seName))
        {
            Debug.Log(seName + "という名前のSEがありません");
            return;
        }

        seSource.Stop();
        seSource.clip = seDic[seName] as AudioClip;
        seSource.Play();
    }
}
