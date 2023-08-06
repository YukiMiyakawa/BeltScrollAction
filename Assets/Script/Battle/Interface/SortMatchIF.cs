using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// レイヤー制御クラス
/// エフェクトを追加した際このクラスを用いてキャラクターとそのエフェクトのレイヤー数を同じにする制御を行いたい
/// </summary>
public class SortMatchIF : MonoBehaviour
{
    [SerializeField] GameObject goj;
    [SerializeField] int layerNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.gameObject.GetComponent<SortingGroup>())
        {
            transform.gameObject.GetComponent<SortingGroup>().sortingOrder = goj.GetComponent<SortingGroup>().sortingOrder + layerNum;
        }
        else
        {
            var objSprite = goj.GetComponent<Renderer>();
            objSprite.sortingOrder = goj.GetComponent<SortingGroup>().sortingOrder + layerNum;
        }
    }
}
