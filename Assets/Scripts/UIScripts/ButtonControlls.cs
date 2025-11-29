using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonControlls : MonoBehaviour
{
    public GameObject ActionEndButton;
    public GameObject ChoosePanel;
    public GameObject CheckPanel;

    public int FinishParameter = 0;
    public int NowParameter = 0;

    public int NowSceneChangeNum = 0;


    public void PushButton1()
    {
        CheckPanel.SetActive(true);
    }

    public void PushButton2()
    {
        CheckPanel.SetActive(true);
    }

    public void PushButton3()
    {
        CheckPanel.SetActive(true);
    }

    public void PushButton4()
    {
        CheckPanel.SetActive(true);
    }

    public void PushYes()
    {
        ChoosePanel.SetActive(false);
        CheckPanel.SetActive(false);
        // 現在のパラメーター数値を取得
        NowParameter = GameManager.GetParameter();
        // 一時的に保持していた数値を取得
        FinishParameter = GameManager.GetAddParameter();
        NowParameter += FinishParameter;
        // 合算した最終数値をゲームマネージャーに受け渡す
        GameManager.SetParameter(NowParameter);
        // 数値の初期化
        NowParameter = 0;
        GameManager.SetAddParameter(NowParameter);
        // シーン遷移で使用する数値
        NowSceneChangeNum = GameManager.GetNowSceneNum();
        NowSceneChangeNum++;
        GameManager.SetNowSceneNum(NowSceneChangeNum);

        // スウィッチ文を使ってシーン遷移を作成
        switch(NowSceneChangeNum)
        {
            case 1:
                SceneManager.LoadScene("Day2");
                break;
            case 2:
                SceneManager.LoadScene("Day3");
                break;
            case 3:
                SceneManager.LoadScene("Day4");
                break;
            case 4:
                SceneManager.LoadScene("Day5");
                break;
            case 5:
                SceneManager.LoadScene("Day6");
                break;
            case 6:
                SceneManager.LoadScene("Day7");
                break;
            default:
                NowSceneChangeNum = 0;
                SceneManager.LoadScene("EndScene");
                break;
        }
        

    }

    public void PushNo()
    {
        // 一時的に保持していた数値を取得
        NowParameter = GameManager.GetAddParameter();
        NowParameter = 0;
        GameManager.SetAddParameter(NowParameter);
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

}
