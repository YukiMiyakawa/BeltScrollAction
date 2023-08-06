using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤー設定情報管理クラス
/// 作成途中
/// </summary>
public static class PlayerDataManager
{
    //戦闘シーン移行時一番初めの操作説明を表示させるか否かのフラグ
    static bool firstBattleFlg = true;

    public static bool GetFirstFlg()
    {
        return firstBattleFlg;
    }
    public static void FirstBatlleOn()
    {
        firstBattleFlg = false;
    }

    //音量設定
    static float bgm = 0.7f, se = 0.5f;
}
