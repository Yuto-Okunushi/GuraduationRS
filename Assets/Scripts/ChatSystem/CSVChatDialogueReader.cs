using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.EventSystems;

public class CSVChatDialogueReader : MonoBehaviour
{
    [Header("CSVファイル (TextAsset)")]
    public TextAsset chatCSV;

    [Header("プレハブと親")]
    public GameObject partnerMessagePrefab;
    public GameObject myMessagePrefab;
    public Transform partnerParent;
    public Transform myParent;

    [Header("スクロールビュー（任意）")]
    public ScrollRect scrollRect;

    private List<string> activeMessages = new List<string>();
    private int currentIndex = 0;
    private bool isActive = false;

    void Update()
    {
        if (!isActive) return;

        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) &&
            EventSystem.current.currentSelectedGameObject == null)
        {
            ShowNextMessage();
        }
    }

    /// <summary>
    /// 指定行・列から length 件の会話を読み込み、Enterで1つずつ表示する
    /// </summary>
    public void StartDialogueFrom(int startRow, int messageColumnIndex, int length)
    {
        activeMessages.Clear();

        if (chatCSV == null)
        {
            Debug.LogError("CSVが設定されていません");
            return;
        }

        using (StringReader reader = new StringReader(chatCSV.text))
        {
            int currentRow = 0;
            while (reader.Peek() > -1 && activeMessages.Count < length)
            {
                string line = reader.ReadLine();
                if (currentRow++ < startRow) continue;
                if (string.IsNullOrWhiteSpace(line)) continue;

                string[] cols = line.Split(',');
                if (messageColumnIndex < cols.Length)
                {
                    string msg = cols[messageColumnIndex].Trim();
                    if (!string.IsNullOrEmpty(msg))
                        activeMessages.Add(msg);
                }
            }
        }

        currentIndex = 0;
        isActive = activeMessages.Count > 0;

        Debug.Log($"チャット開始：{activeMessages.Count} 件の会話（行 {startRow}, 列 {messageColumnIndex}）");
    }

    void ShowNextMessage()
    {
        if (currentIndex >= activeMessages.Count)
        {
            isActive = false;
            Debug.Log("チャット終了");
            return;
        }

        string message = activeMessages[currentIndex];
        bool isPartner = currentIndex % 2 == 0;

        GameObject prefab = isPartner ? partnerMessagePrefab : myMessagePrefab;
        Transform parent = isPartner ? partnerParent : myParent;

        GameObject bubble = Instantiate(prefab, parent);
        Text text = bubble.GetComponentInChildren<Text>();
        if (text != null) text.text = message;

        currentIndex++;

        if (scrollRect != null)
        {
            Canvas.ForceUpdateCanvases();
            scrollRect.verticalNormalizedPosition = 0f;
        }
    }

    public bool IsDialogueActive() => isActive;
}
