using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
	//ボリューム保存用のkeyとデフォルト値
	private const string BGM_VOLUME_KEY = "BGM_VOLUME_KEY";
	private const string SE_VOLUME_KEY = "SE_VOLUME_KEY";
	private const float BGM_VOLUME_DEFULT = 0.5f;
	private const float SE_VOLUME_DEFULT = 0.7f;

	public float GetDefultBGMVolume() { return BGM_VOLUME_DEFULT; }
    public float GetDefultSEVolume() { return SE_VOLUME_DEFULT; }

    //BGMがフェードするのにかかる時間
    public const float BGM_FADE_SPEED_RATE_HIGH = 0.9f;
	public const float BGM_FADE_SPEED_RATE_LOW = 0.3f;
	private float bgmFadeSpeedRate = BGM_FADE_SPEED_RATE_HIGH;

	//次流すBGM名、SE名
	private string nextBGMName;
	private string nextSEName;

	//BGMをフェードアウト中か
	private bool isFadeOut = false;

	//BGM用、SE用に分けてオーディオソースを持つ
	public AudioSource AttachBGMSource, AttachSESource;

	//全Audioを保持
	private Dictionary<string, AudioClip> bgmDic, seDic;

	//=================================================================================
	//初期化
	//=================================================================================

	private void Awake()
	{
		if (this != Instance)
		{
			Destroy(this);
			return;
		}

		DontDestroyOnLoad(this.gameObject);

		//リソースフォルダから全SE&BGMのファイルを読み込みセット
		bgmDic = new Dictionary<string, AudioClip>();
		seDic = new Dictionary<string, AudioClip>();

		object[] bgmList = Resources.LoadAll("Audio/BGM");
		object[] seList = Resources.LoadAll("Audio/SE");

		foreach (AudioClip bgm in bgmList)
		{
			bgmDic[bgm.name] = bgm;
		}
		foreach (AudioClip se in seList)
		{
			seDic[se.name] = se;
		}
		PlayBGM("Battle1");
	}

	private void Start()
	{
		Application.targetFrameRate = 60;

		AttachBGMSource.volume = PlayerPrefs.GetFloat(BGM_VOLUME_KEY, BGM_VOLUME_DEFULT);
		AttachSESource.volume = PlayerPrefs.GetFloat(SE_VOLUME_KEY, SE_VOLUME_DEFULT);
	}

	//=================================================================================
	//SE
	//=================================================================================

	/// <summary>
	/// 指定したファイル名のSEを流す。第二引数のdelayに指定した時間だけ再生までの間隔を空ける
	/// </summary>
	public void PlaySE(string seName, float delay = 0.0f)
	{
		if (!seDic.ContainsKey(seName))
		{
			Debug.Log(seName + "という名前のSEがありません");
			return;
		}

		nextSEName = seName;
		Invoke("DelayPlaySE", delay);
	}

	private void DelayPlaySE()
	{
		AttachSESource.PlayOneShot(seDic[nextSEName] as AudioClip);
	}

	//=================================================================================
	//BGM
	//=================================================================================

	/// <summary>
	/// 指定したファイル名のBGMを流す。ただし既に流れている場合は前の曲をフェードアウトさせてから。
	/// </summary>
	public void PlayBGM(string bgmName, bool loop = true)
	{
		if (!bgmDic.ContainsKey(bgmName))
		{
			Debug.Log(bgmName + "という名前のBGMがありません");
			return;
		}

		//現在BGMが流れていない時はそのまま流す
		if (!AttachBGMSource.isPlaying)
		{
			nextBGMName = "";
			AttachBGMSource.clip = bgmDic[bgmName] as AudioClip;
			AttachBGMSource.loop = loop;
			AttachBGMSource.Play();
		}
		//違うBGMが流れている時は、流れているBGMをフェードアウトさせてから次を流す。同じBGMが流れている時はスルー
		else if (AttachBGMSource.clip.name != bgmName)
		{
			nextBGMName = bgmName;
			FadeOutBGM(BGM_FADE_SPEED_RATE_HIGH);
		}

	}

	/// <summary>
	/// 現在流れている曲をフェードアウトさせる
	/// fadeSpeedRateに指定した割合でフェードアウトするスピードが変わる
	/// </summary>
	public void FadeOutBGM(float fadeSpeedRate = BGM_FADE_SPEED_RATE_LOW)
	{
		bgmFadeSpeedRate = fadeSpeedRate;
		isFadeOut = true;
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
			AttachBGMSource.volume = PlayerPrefs.GetFloat(BGM_VOLUME_KEY, BGM_VOLUME_DEFULT);
			isFadeOut = false;

			if (!string.IsNullOrEmpty(nextBGMName))
			{
				PlayBGM(nextBGMName);
			}
		}

	}

	//=================================================================================
	//音量変更
	//=================================================================================

	/// <summary>
	/// BGMとSEのボリュームを別々に変更
	/// </summary>
	public void ChangeVolume(float BGMVolume, float SEVolume)
	{
		AttachBGMSource.volume = BGMVolume;
		AttachSESource.volume = SEVolume;
	}

    /// <summary>
    /// BGMとSEのボリュームを別々に変更&保存
    /// </summary>
    public void SaveChangeVolume(float BGMVolume, float SEVolume)
    {
        AttachBGMSource.volume = BGMVolume;
        AttachSESource.volume = SEVolume;

        PlayerPrefs.SetFloat(BGM_VOLUME_KEY, BGMVolume);
        PlayerPrefs.SetFloat(SE_VOLUME_KEY, SEVolume);
    }
}
