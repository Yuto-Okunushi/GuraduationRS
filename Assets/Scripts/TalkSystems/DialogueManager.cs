using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public CSVDialogueReader dialogueReader;

    public int startRow = 1;            // �J�n����s�i0��1�s�ځj
    public int nameColumnIndex = 1;     // �L�����N�^�[���̗�i0��1��ځj
    public int messageColumnIndex = 3;  // ��b���e�̗�i0��1��ځj

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
