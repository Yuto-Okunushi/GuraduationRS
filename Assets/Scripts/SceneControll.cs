using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControll : MonoBehaviour
{
    // 遷移先のシーン名
    public string nextSceneName;

    // ボタンなどから呼び出す用の関数
    public void GoToNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("遷移先のシーン名が設定されていません");
        }
    }

    // 例：トリガーで遷移する場合（オプション）
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // スペースキーで遷移
        {
            GoToNextScene();
        }
    }
}
