using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public CSVDialogueReader dialogueReader;

    public int startRow = 1;            // 開始する行（0が1行目）
    public int nameColumnIndex = 1;     // キャラクター名の列（0が1列目）
    public int messageColumnIndex = 3;  // 会話内容の列（0が1列目）

    void Start()
    {
        dialogueReader.StartDialogueFrom(startRow, nameColumnIndex, messageColumnIndex);
    }

    void Update()
    {
        if (dialogueReader.IsDialogueActive())
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
            {
                dialogueReader.Next();
            }
        }
    }
}
