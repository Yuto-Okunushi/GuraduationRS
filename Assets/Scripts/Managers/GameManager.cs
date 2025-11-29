using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    // インスタンス作成
    public static GameManager instance = null;

    // ゲームマネージャー内メンバー変数

    // シーン遷移で使われる数値
    public int sceneChamgeNum = 0;

    // ストーリー分岐で使用されるパラメーターに新しく足される変数
    public int addParameter = 0;

    // 一時的に数値を保持する変数
    public int setAddParameter = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    //==Getter====================================================================
    static public int GetParameter()
    {
        // ストーリーパラメータの取得
        return instance.addParameter;
    }

    static public int GetAddParameter()
    {
        return instance.setAddParameter;
    }

    static public int GetNowSceneNum()
    {
        return instance.sceneChamgeNum;
    }
    
    //==Setter==========================================================================
    static public void SetParameter(int value)
    {
        instance.addParameter = value;
    }

    static public void SetAddParameter(int value)
    {
        instance.setAddParameter = value;
    }

    static public void SetNowSceneNum(int value)
    {
        instance.sceneChamgeNum = value;
    }
    
}
