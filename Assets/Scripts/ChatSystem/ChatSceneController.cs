using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatSceneController : MonoBehaviour
{
    public CSVChatDialogueReader chatReader;

    public int startRow = 0;
    public int messageColumnIndex = 0;
    public int length = 0;


    void Start()
    {
        // ��F���̃V�[���ł�CSV��3��ڂ�10�s�ڂ���6�����̉�b��\��
        chatReader.StartDialogueFrom(startRow, messageColumnIndex, length);
    }
}
