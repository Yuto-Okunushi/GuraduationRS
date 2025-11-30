using UnityEngine;

public class ButtonParameter : MonoBehaviour
{
    [Header("パラメーター設定")]
    [SerializeField] private int parameterDelta = 0;
    [Header("対応したリアクションを再生")]
    [Header("行　横")]
    [SerializeField] private int startRow = 0;
    [Header("列　縦")]
    [SerializeField] private int nameColumnIndex = 0;
    [SerializeField] private int messageColumnIndex = 0;

    public void OnClick()
    {
        // 数値を一時出来にゲームマネージャーに送信する
        GameManager.SetAddParameter(parameterDelta);

        // CSV関連
        GameManager.SetCSVRow(startRow);
        GameManager.SetCSVnameColumnIndex(nameColumnIndex);
        GameManager.SetCSVmessageColumnIndex(messageColumnIndex);
    }
}
