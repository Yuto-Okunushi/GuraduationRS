using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ChoiceLogger : MonoBehaviour
{
    // �v���C���[���I�񂾑I������ۑ�����
    private List<ChoiceRecord> choiceLog = new List<ChoiceRecord>();

    [System.Serializable]
    public class ChoiceRecord
    {
        public int questionNumber;   // ����ԍ�
        public string selectedChoice; // �v���C���[���I�񂾑I����

        public ChoiceRecord(int qNum, string choice)
        {
            questionNumber = qNum;
            selectedChoice = choice;
        }
    }

    // �v���C���[�̑I����o�^����
    public void RecordChoice(int questionNumber, string selectedChoice)
    {
        choiceLog.Add(new ChoiceRecord(questionNumber, selectedChoice));
        Debug.Log($"Q{questionNumber}�Łu{selectedChoice}�v��I�т܂���");
    }

    // �ŏI�I��CSV�ɏ����o��
    public void ExportChoicesToCSV(string filename = "PlayerChoices.csv")
    {
        string path = Path.Combine(Application.persistentDataPath, filename);
        using (StreamWriter writer = new StreamWriter(path, false))
        {
            writer.WriteLine("QuestionNumber,SelectedChoice");

            foreach (var record in choiceLog)
            {
                writer.WriteLine($"{record.questionNumber},{record.selectedChoice}");
            }
        }

        Debug.Log("CSV�o�͊���: " + path);
    }

    // �� �e�X�g�p�F�X�y�[�X�L�[�őI�����L�^�AE�L�[�ŏo��
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int q = choiceLog.Count + 1;
            string testChoice = "A"; // ���̑I����
            RecordChoice(q, testChoice);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            ExportChoicesToCSV();
        }
    }
}
