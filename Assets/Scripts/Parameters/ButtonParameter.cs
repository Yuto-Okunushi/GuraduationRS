using UnityEngine;

public class ButtonParameter : MonoBehaviour
{
    [Header("パラメーター設定")]
    [SerializeField] private int parameterDelta = 0;

    [Header("チャットCSV設定")]
    [SerializeField] private CSVChatDialogueReader chatReader; // さっきのスクリプト
    [SerializeField] private int startRow = 0;
    [SerializeField] private int messageColumnIndex = 0;
    [SerializeField] private int length = 4; // この会話ブロックの行数

    public void OnClick()
    {
        // ① パラメータ加算
        int current = GameManager.GetParameter();
        current += parameterDelta;
        GameManager.SetParameter(current);

        // ② チャット再生開始
        if (chatReader != null)
        {
            chatReader.StartDialogueFrom(startRow, messageColumnIndex, length);
        }
        else
        {
            Debug.LogWarning("ButtonParameter: chatReader が設定されていません");
        }
    }
}
