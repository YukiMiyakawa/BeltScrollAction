using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 無限スクロールバトルステージ制御クラス
/// </summary>
public class StageScrollController : MonoBehaviour
{
    [SerializeField] GameObject[] stages; //ステージは左から順番に配列に格納すること
    [SerializeField] Transform leftWall;
    [SerializeField] Transform rightWall;
    [SerializeField] Text clearText;
    Transform firstStageTransform;
    Transform secondStageTransform;
    Transform thirdStageTransform;

    Transform playerTransform;

    private const float STAGE_DISTANCE = 9f;
    private const float FIRST_STAGE_DISTANCE_TO_THIRD_STAGE = 27f;
    private const float LEFT_WALL_TO_FIRST_STAGE = -5f;
    private const float RIGHT_WALL_TO_FIRST_STAGE = 23f;
    private const float BETWEEN_FIRST_AND_SECOND = 13.5f;

    [SerializeField] GameObject ahyaKife;
    [SerializeField] GameObject ahyaGun;
    List<GameObject> enemys;
    int destroyEnemyCount;

    // Start is called before the first frame update
    void Start()
    {
        enemys = new List<GameObject>();

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        firstStageTransform = stages[0].transform;
        secondStageTransform = stages[1].transform;
        thirdStageTransform = stages[2].transform;

        //一番初めの敵キャラ生成
        var secondPos = secondStageTransform.position;
        enemys.Add(Instantiate(ahyaKife, secondPos, Quaternion.Euler(0, 180, 0)));
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var enemy in enemys)
        {
            if (enemy == null) destroyEnemyCount++;
        }

        //destroyEnemyCount += enemys.Count(e => e == null);
        enemys.RemoveAll(s => s == null);
        if (enemys.Count > 0) return;

        StageMoveWatch();
    }

    /// <summary>
    /// 横方向に並ぶ三つの区画のステージ背景を用意し、右方向へ移動するにつれて
    /// 左の区画のステージ背景を一番右区画に配置していく
    /// </summary>
    void StageMoveWatch()
    {
        if(firstStageTransform.position.x + BETWEEN_FIRST_AND_SECOND < playerTransform.position.x)
        {
            //２つ目のステージ区画に移動した時１つ目のステージを３つ目に移動する
            Vector3 pos = firstStageTransform.position;
            pos.x += FIRST_STAGE_DISTANCE_TO_THIRD_STAGE;
            firstStageTransform.position = pos;

            //ステージ配列の順番を入れ替える
            StageOrderChange();

            if (clearText) clearText.text = $"{destroyEnemyCount}体撃破しました。";
        }
    }

    /// <summary>
    /// ステージ背景を格納する配列の順番を入れ替える
    /// </summary>
    void StageOrderChange()
    {
        var TmpStage1 = firstStageTransform;
        firstStageTransform = secondStageTransform;
        secondStageTransform = thirdStageTransform;
        thirdStageTransform = TmpStage1;

        var firstPos = leftWall.position;
        var tmpPos = firstPos;

        tmpPos.x = firstStageTransform.position.x + LEFT_WALL_TO_FIRST_STAGE;
        leftWall.position = tmpPos;

        firstPos.x = firstStageTransform.position.x + RIGHT_WALL_TO_FIRST_STAGE;
        rightWall.position = firstPos;

        InstantiateEnemy();
    }

    /// <summary>
    /// 1～3の敵出現パターンを用意し、ランダムで呼び出す
    /// </summary>
    void InstantiateEnemy()
    {
        int n = Random.Range(1, 3);

        switch(n)
        {
            case 1:
                InstantiatePattern1();
                break;
            case 2:
                InstantiatePattern2();
                break;
            case 3:
                InstantiatePattern3();
                break;
            default:
                Debug.LogWarning("想定範囲外のランダム数値です");
                break;
        }
    }

    void InstantiatePattern1()
    {
        var thirdPos = thirdStageTransform.position;
        enemys.Add(Instantiate(ahyaKife, thirdPos, Quaternion.Euler(0, 180, 0)));
        thirdPos.y = 0.2f;
        thirdPos.x -= 4f;
        enemys.Add(Instantiate(ahyaKife, thirdPos, Quaternion.Euler(0, 180, 0)));
        var firstPos = firstStageTransform.position;
        firstPos.x += 6;
        enemys.Add(Instantiate(ahyaGun, firstPos, Quaternion.Euler(0, 0, 0)));
    }

    void InstantiatePattern2()
    {
        var thirdPos = thirdStageTransform.position;
        enemys.Add(Instantiate(ahyaKife, thirdPos, Quaternion.Euler(0, 180, 0)));
        thirdPos.y = 0.2f;
        thirdPos.x -= 4f;
        enemys.Add(Instantiate(ahyaKife, thirdPos, Quaternion.Euler(0, 180, 0)));
        thirdPos.y = -0.5f;
        thirdPos.x -= 3f;
        enemys.Add(Instantiate(ahyaKife, thirdPos, Quaternion.Euler(0, 180, 0)));
    }

    void InstantiatePattern3()
    {
        var thirdPos = thirdStageTransform.position;
        thirdPos.x -= 5f;
        enemys.Add(Instantiate(ahyaKife, thirdPos, Quaternion.Euler(0, 180, 0)));
        thirdPos.y = 0.2f;
        thirdPos.x -= 3f;
        enemys.Add(Instantiate(ahyaKife, thirdPos, Quaternion.Euler(0, 180, 0)));
    }

    public float GetFirstStagePositionX()
    {
        return firstStageTransform.position.x;
    }

    public float GetThirdStagePositionX()
    {
        return thirdStageTransform.position.x;
    }

    public int GetDestroyEnemys()
    {
        return destroyEnemyCount;
    }
}
