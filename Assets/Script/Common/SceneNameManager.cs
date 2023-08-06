using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// シーン名管理クラス
/// </summary>
public class SceneNameManager : MonoBehaviour
{
    protected enum NextSceneName
    {
        Title, Home, Scene1_1, Scene1_2, Battle1, ScrollBattle, Config
    }
    [SerializeField] protected NextSceneName nextSceneName;
}
