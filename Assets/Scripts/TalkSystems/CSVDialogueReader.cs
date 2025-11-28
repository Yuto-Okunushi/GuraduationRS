using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class CSVDialogueReader : MonoBehaviour
{
    public Text nameText;
    public Text messageText;
    public GameObject dialoguePanel;
    public TextAsset csvFile;

    private List<(string name, string message)> dialogueLines = new List<(string, string)>();
    private int currentIndex = 0;
    private bool isActive = false;

    private bool inputLocked = false;
    private string currentTalkCharacterName = "";
    private TalkTracker talkTracker;

    // ======================================================
    //   修正版：CSV の 1 行を安全に解析するパーサー
    // ======================================================
    private List<string> ParseCsvLine(string line)
    {
        List<string> result = new List<string>();
        bool inQuotes = false;
        string value = "";

        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];

            if (c == '\"')
            {
                // "" → " として扱う
                if (inQuotes && i + 1 < line.Length && line[i + 1] == '\"')
                {
                    value += '\"';
                    i++; // 1文字スキップ
                }
                else
                {
                    inQuotes = !inQuotes;
                }
            }
            else if (c == ',' && !inQuotes)
            {
                result.Add(value);
                value = "";
            }
            else
            {
                value += c;
            }
        }

        result.Add(value);
        return result;
    }

    void Update()
    {
        if (!isActive) return;

        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0)) && !inputLocked)
        {
            Next();
        }

        if (Input.GetKeyUp(KeyCode.Return) || Input.GetMouseButtonUp(0))
        {
            //inputLocked = false;
        }
    }

    public void StartDialogueFrom(int startRow, int nameColumnIndex, int messageColumnIndex, string characterName = "", TalkTracker tracker = null)
    {
        dialogueLines.Clear();

        using (StringReader reader = new StringReader(csvFile.text))
        {
            int lineCount = 0;
            while (reader.Peek() > -1)
            {
                string rawLine = reader.ReadLine();

                if (lineCount < startRow)
                {
                    lineCount++;
                    continue;
                }

                if (string.IsNullOrWhiteSpace(rawLine))
                {
                    dialogueLines.Add(("", ""));
                    lineCount++;
                    continue;
                }

                // 🔥 修正版パーサーで解析
                var parts = ParseCsvLine(rawLine);

                string name = nameColumnIndex < parts.Count ? parts[nameColumnIndex].Trim() : "";
                string message = messageColumnIndex < parts.Count ? parts[messageColumnIndex].Trim() : "";

                dialogueLines.Add((name, message));
                lineCount++;
            }
        }

        currentIndex = 0;
        isActive = true;
        inputLocked = true;
        dialoguePanel.SetActive(true);

        currentTalkCharacterName = characterName;
        talkTracker = tracker;

        ShowCurrentLine();
    }

    public void Next()
    {
        currentIndex++;

        if (currentIndex >= dialogueLines.Count)
        {
            EndDialogue();
            return;
        }

        var (name, message) = dialogueLines[currentIndex];

        if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(message))
        {
            EndDialogue();
            return;
        }

        ShowCurrentLine();
    }

    void ShowCurrentLine()
    {
        var (name, message) = dialogueLines[currentIndex];
        nameText.text = name;
        messageText.text = message;
    }

    void EndDialogue()
    {
        isActive = false;
        dialoguePanel.SetActive(false);

        if (talkTracker != null && !string.IsNullOrEmpty(currentTalkCharacterName))
        {
            talkTracker.OnDialogueFinished(currentTalkCharacterName);
        }
    }

    public bool IsDialogueActive()
    {
        return isActive;
    }
}
