using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// キャラクター表示順制御クラス
/// </summary>
public class SortrLayerController : MonoBehaviour
{
    [SerializeField] private List<GameObject> units;

    // Update is called once per frame
    void Update()
    {
        units.RemoveAll(s => s == null);
        if (units.Count == 0) return;

        //キャラクターの中心点でソートしているので足元でソートを行うよう後ほど調整
        //現状違和感を感じないからこのままでもよいかも
        units.Sort((a, b) => a.transform.position.y.CompareTo(b.transform.position.y));
        units.Reverse();
        int layer = 0;
        foreach (var unit in units)
        {
            if (unit.GetComponent<SortingGroup>())
            {
                unit.GetComponent<SortingGroup>().sortingOrder = layer++;
            }
            else
            {
                var　objSprite = unit.GetComponent<Renderer>();
                objSprite.sortingOrder = layer++;
            }

        }
    }

    public void addUnit(GameObject unit)
    {
        units.Add(unit);
    }
}
