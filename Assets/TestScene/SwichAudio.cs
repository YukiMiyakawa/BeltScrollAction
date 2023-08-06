using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwichAudio : MonoBehaviour
{
    //[SerializeField] private AudioSource[] _audios;
    /// <summary>
    /// BGMの混ぜ具合。0ならSound1、1ならSound2になる
    /// </summary>
    //[Range(0, 1)] public float _mixRate = 0;

    [SerializeField] AudioSource battleAudio;
    [SerializeField] AudioSource victoryAudio;
    float waitTime;
    bool battleFinish;

    public void Play()
    {
        //_audios[0].Play();
        //_audios[1].Play();
    }

    private void Start()
    {
        battleAudio.Play();
    }

    private void Update()
    {
        waitTime += Time.deltaTime;

        if(waitTime > 3f)
        {
            if (battleFinish) return;
            battleAudio.Stop();
            victoryAudio.Play();
            battleFinish = true;
        }
    }

    void BgmChange(AudioSource stopBgm, AudioSource startBgm)
    {
        stopBgm.Stop();
        startBgm.Play();
    }
}
