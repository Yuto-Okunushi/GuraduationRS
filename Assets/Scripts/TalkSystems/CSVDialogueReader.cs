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

    void Update()
    {
        if (!isActive) return;

        // �G���^�[�L�[�܂��̓}�E�X�N���b�N�Ŏ��̃Z���t��
        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0)) && !inputLocked)
        {
            Next();
            //inputLocked = true;
        }

        // �L�[/�N���b�N�𗣂����烍�b�N�����i�������ςȂ��΍�j
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

        currentIndex = 0;
        isActive = true;
        inputLocked = true; // �ŏ��̃N���b�N���X�L�b�v����ꍇtrue
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
