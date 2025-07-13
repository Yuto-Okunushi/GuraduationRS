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

    public void StartDialogueFrom(int startRow, int nameColumnIndex, int messageColumnIndex)
    {
        dialogueLines.Clear();

        using (StringReader reader = new StringReader(csvFile.text))
        {
            int lineCount = 0;
            while (reader.Peek() > -1)
            {
                string line = reader.ReadLine();

                if (lineCount < startRow)
                {
                    lineCount++;
                    continue;
                }

                if (string.IsNullOrWhiteSpace(line))
                {
                    dialogueLines.Add(("", ""));
                    continue;
                }

                string[] parts = line.Split(',');

                string name = nameColumnIndex < parts.Length ? parts[nameColumnIndex].Trim() : "";
                string message = messageColumnIndex < parts.Length ? parts[messageColumnIndex].Trim() : "";

                dialogueLines.Add((name, message));
                lineCount++;
            }
        }

        if (dialogueLines.Count == 0)
        {
            Debug.LogWarning("読み込まれた会話データが空です。");
            return;
        }

        currentIndex = 0;
        isActive = true;
        dialoguePanel.SetActive(true);
        ShowCurrentLine();
    }

    public void Next()
    {
        if (!isActive) return;

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
    }

    public bool IsDialogueActive()
    {
        return isActive;
    }
}
