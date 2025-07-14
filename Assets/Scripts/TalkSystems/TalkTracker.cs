using System.Collections.Generic;
using UnityEngine;

public class TalkTracker : MonoBehaviour
{
    public List<string> characterNames;             // 会話対象キャラクター名（例: "Santa", "Snowman"）
    private HashSet<string> talkedCharacters = new HashSet<string>();

    public CSVDialogueReader dialogueReader;        // 最後に読み込む会話用
    public TextAsset nextCsv;                       // チュートリアル終了後のCSV
    public int nextStartRow = 0;
    public int nameCol = 1;
    public int messageCol = 3;

    public void OnDialogueFinished(string characterName)
    {
        if (!talkedCharacters.Contains(characterName))
        {
            talkedCharacters.Add(characterName);
            Debug.Log($"会話終了: {characterName}");

            if (talkedCharacters.Count >= characterNames.Count)
            {
                Debug.Log("全員との会話が完了しました！");
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
