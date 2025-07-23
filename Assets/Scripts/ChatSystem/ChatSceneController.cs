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
        // 例：このシーンではCSVの3列目の10行目から6件分の会話を表示
        chatReader.StartDialogueFrom(startRow, messageColumnIndex, length);
    }
}
