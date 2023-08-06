using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

/// <summary>
/// キャラクター立ち絵取得クラス
/// </summary>
public class CharaImageBehavior : MonoBehaviour
{
    SpriteAtlas monaAtlas;
    SpriteAtlas gicoAtlas;
    SpriteAtlas presidentAtlas;

    //全Atlasを保持
    private Dictionary<string, SpriteAtlas> alDic;

    private void Awake()
    {
        //リソースフォルダからAtlasのファイルを読み込みセット
        alDic = new Dictionary<string, SpriteAtlas>();

        object[] alList = Resources.LoadAll("ADV/CharaImage/Atlas");

        foreach (SpriteAtlas bgm in alList)
        {
            alDic[bgm.name] = bgm;
        }
    }

    void Update()
    {
        
    }

    public SpriteAtlas GetAtlas(string name)
    {
        if (!alDic.ContainsKey(name))
        {
            Debug.Log(name + "という名前のSEがありません");
            return null;
        }

        return alDic[name] as SpriteAtlas;
    }
}
