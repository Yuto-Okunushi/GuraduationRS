using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ChoiceLogger : MonoBehaviour
{
    // プレイヤーが選んだ選択肢を保存する
    private List<ChoiceRecord> choiceLog = new List<ChoiceRecord>();

    [System.Serializable]
    public class ChoiceRecord
    {
        public int questionNumber;   // 質問番号
        public string selectedChoice; // プレイヤーが選んだ選択肢

        public ChoiceRecord(int qNum, string choice)
        {
            questionNumber = qNum;
            selectedChoice = choice;
        }
    }

    // プレイヤーの選択を登録する
    public void RecordChoice(int questionNumber, string selectedChoice)
    {
        choiceLog.Add(new ChoiceRecord(questionNumber, selectedChoice));
        Debug.Log($"Q{questionNumber}で「{selectedChoice}」を選びました");
    }

    // 最終的にCSVに書き出す
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

        Debug.Log("CSV出力完了: " + path);
    }

    // ★ テスト用：スペースキーで選択肢記録、Eキーで出力
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int q = choiceLog.Count + 1;
            string testChoice = "A"; // 仮の選択肢
            RecordChoice(q, testChoice);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            ExportChoicesToCSV();
        }
    }
}
