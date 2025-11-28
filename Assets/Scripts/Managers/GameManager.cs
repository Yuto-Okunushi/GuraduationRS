using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    // インスタンス作成
    public static GameManager instance = null;

    // ゲームマネージャー内メンバー変数

    // ストーリー分岐、最終評価に使われるパラメーター
    int storyParameter = 0;

    // 選択肢で使われる数値達
    int kenin = 0;
    int kouryo = 0;
    int taiou = 0;
    int henkaku = 0;
    int ronri = 0;
    int zikogisei = 0;
    int ikusei = 0;

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

    
    //==Setter==========================================================================

    
}
