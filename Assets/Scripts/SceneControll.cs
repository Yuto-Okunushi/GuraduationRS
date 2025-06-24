using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControll : MonoBehaviour
{
    // �J�ڐ�̃V�[����
    public string nextSceneName;

    // �{�^���Ȃǂ���Ăяo���p�̊֐�
    public void GoToNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("�J�ڐ�̃V�[�������ݒ肳��Ă��܂���");
        }
    }

    // ��F�g���K�[�őJ�ڂ���ꍇ�i�I�v�V�����j
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // �X�y�[�X�L�[�őJ��
        {
            GoToNextScene();
        }
    }
}
