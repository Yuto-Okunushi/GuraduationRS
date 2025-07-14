using System.Collections.Generic;
using UnityEngine;

public class TalkTracker : MonoBehaviour
{
    public List<string> characterNames;             // ��b�ΏۃL�����N�^�[���i��: "Santa", "Snowman"�j
    private HashSet<string> talkedCharacters = new HashSet<string>();

    public CSVDialogueReader dialogueReader;        // �Ō�ɓǂݍ��މ�b�p
    public TextAsset nextCsv;                       // �`���[�g���A���I�����CSV
    public int nextStartRow = 0;
    public int nameCol = 1;
    public int messageCol = 3;

    public void OnDialogueFinished(string characterName)
    {
        if (!talkedCharacters.Contains(characterName))
        {
            talkedCharacters.Add(characterName);
            Debug.Log($"��b�I��: {characterName}");

            if (talkedCharacters.Count >= characterNames.Count)
            {
                Debug.Log("�S���Ƃ̉�b���������܂����I");
                StartNextDialogue();
            }
        }
    }

    void StartNextDialogue()
    {
        if (dialogueReader != null && nextCsv != null)
        {
            dialogueReader.csvFile = nextCsv;
            dialogueReader.StartDialogueFrom(nextStartRow, nameCol, messageCol);
        }
    }
}
