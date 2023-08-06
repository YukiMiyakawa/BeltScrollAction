using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// デバッグ用クラス
/// </summary>
public class GameBehavior : MonoBehaviour
{
    [SerializeField] AudioManager audioManager;
    [SerializeField] string audioName;

    [SerializeField] Transform player;
    [SerializeField] Transform enemy;

    Vector2 playerPos;
    Vector2 enemyPos;

    [SerializeField] Text playerTrn;
    [SerializeField] Text enemyTrn;
    [SerializeField] Text dis;
    [SerializeField] Text magDis;

    public bool disCheck;


    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;

        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponentInChildren<AudioManager>();
        if (audioName != "") audioManager.PlayBGM($"{audioName}");


    }


    private void Update()
    {
        if (disCheck)
        {
            playerTrn.text = player.position.ToString();
            enemyTrn.text = enemy.position.ToString();
            dis.text = Vector2.Distance(player.position, enemy.position).ToString();
            magDis.text = (player.position - enemy.position).magnitude.ToString();
        }
        
    }

}
