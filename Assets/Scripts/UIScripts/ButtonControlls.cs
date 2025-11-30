using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonControlls : MonoBehaviour
{
    public GameObject ActionEndButton;
    public GameObject ChoosePanel;

    [Header("表示パネル類")]
    public GameObject CheckPanel;
    public GameObject TalkPanel;

    [Header("表示するテキスト類")]
    public Text talkText;
    public Text characterTxet;

    [Header("CSVファイル (TextAsset)")]
    public TextAsset chatCSV;

    public int FinishParameter = 0;
    public int NowParameter = 0;

    public int NowSceneChangeNum = 0;

    public int CSVRow = 0;
    public int CSVnameColumnIndex = 0;
    public int CSVmessageColumnIndex = 0;

    // CSV関連
    private int currentRow = 0;
    private string[] csvLines;
    private bool isTalking = false;


    //===========================================================
    // 空行判定（完全版）
    //===========================================================
    private bool IsEmptyRow(string line)
    {
        if (line == null) return true;

        // 前後の改行・空白を除去
        string trimmed = line.Trim();

        // 完全に空白なら空行
        if (string.IsNullOrWhiteSpace(trimmed)) return true;

        // カンマ区切りにして各列をチェック
        string[] cols = trimmed.Split(',');

        foreach (string col in cols)
        {
            if (!string.IsNullOrWhiteSpace(col))
            {
                return false; // 中身がある列が1つでもあれば空行ではない
            }
        }

        // 全列が空白 → 実質的に空行
        return true;
    }


    //===========================================================
    // ボタン
    //===========================================================
    public void PushButton1() { CheckPanel.SetActive(true); }
    public void PushButton2() { CheckPanel.SetActive(true); }
    public void PushButton3() { CheckPanel.SetActive(true); }
    public void PushButton4() { CheckPanel.SetActive(true); }


    //===========================================================
    // Yes → 会話開始
    //===========================================================
    public void PushYes()
    {
        ChoosePanel.SetActive(false);
        CheckPanel.SetActive(false);

        // パラメータ加算
        NowParameter = GameManager.GetParameter();
        FinishParameter = GameManager.GetAddParameter();
        NowParameter += FinishParameter;
        GameManager.SetParameter(NowParameter);

        GameManager.SetAddParameter(0);

        // シーン遷移用
        NowSceneChangeNum = GameManager.GetNowSceneNum();
        NowSceneChangeNum++;
        GameManager.SetNowSceneNum(NowSceneChangeNum);

        // CSV設定
        CSVRow = GameManager.GetCSVRow();
        CSVnameColumnIndex = GameManager.GetCSVnameColumnIndex();
        CSVmessageColumnIndex = GameManager.GetCSVmessageColumnIndex();

        // 会話パネル表示
        TalkPanel.SetActive(true);

        // CSV読み込み開始
        StartTalkFromCSV();
    }


    //===========================================================
    // No → キャンセル
    //===========================================================
    public void PushNo()
    {
        GameManager.SetAddParameter(0);
        GameManager.SetCSVRow(0);
        GameManager.SetCSVnameColumnIndex(0);
        GameManager.SetCSVmessageColumnIndex(0);

        CheckPanel.SetActive(false);
    }


    public void PushActionEnd()
    {
        ChoosePanel.SetActive(true);
    }

    public void PushX()
    {
        ChoosePanel.SetActive(false);
    }


    //===========================================================
    // CSV読み取り開始
    //===========================================================
    private void StartTalkFromCSV()
    {
        if (chatCSV == null)
        {
            Debug.LogError("CSVファイルがセットされていません");
            return;
        }

        csvLines = chatCSV.text.Split('\n');
        currentRow = CSVRow;
        isTalking = true;

        ShowLine(currentRow);
    }


    //===========================================================
    // 1行表示
    //===========================================================
    private void ShowLine(int row)
    {
        if (row >= csvLines.Length || IsEmptyRow(csvLines[row]))
        {
            EndTalk();
            return;
        }

        string[] columns = csvLines[row].Split(',');

        if (CSVnameColumnIndex < columns.Length)
            characterTxet.text = columns[CSVnameColumnIndex].Trim();

        if (CSVmessageColumnIndex < columns.Length)
            talkText.text = columns[CSVmessageColumnIndex].Trim();
    }


    //===========================================================
    // Enter / クリックで次へ
    //===========================================================
    private void Update()
    {
        if (!isTalking) return;

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
        {
            currentRow++;

            if (currentRow >= csvLines.Length || IsEmptyRow(csvLines[currentRow]))
            {
                EndTalk();
                return;
            }

            ShowLine(currentRow);
        }
    }


    //===========================================================
    // 終了 → パネル非表示 → シーン遷移
    //===========================================================
    private void EndTalk()
    {
        isTalking = false;
        TalkPanel.SetActive(false);

        // シーン遷移
        switch (NowSceneChangeNum)
        {
            case 1: SceneManager.LoadScene("Day2"); break;
            case 2: SceneManager.LoadScene("Day3"); break;
            case 3: SceneManager.LoadScene("Day4"); break;
            case 4: SceneManager.LoadScene("Day5"); break;
            case 5: SceneManager.LoadScene("Day6"); break;
            case 6: SceneManager.LoadScene("Day7"); break;

            default:
                NowSceneChangeNum = 0;
                SceneManager.LoadScene("EndScene");
                break;
        }
    }
}
