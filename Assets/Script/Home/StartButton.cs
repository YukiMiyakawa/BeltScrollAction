using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//�폜�\��
public class StartButton : MonoBehaviour
{
    public void SceneChange()
    {
        SceneManager.LoadScene("Scene1-1");
    }
}
