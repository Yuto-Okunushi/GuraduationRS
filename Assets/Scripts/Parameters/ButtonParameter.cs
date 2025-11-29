using UnityEngine;

public class ButtonParameter : MonoBehaviour
{
    [Header("パラメーター設定")]
    [SerializeField] private int parameterDelta = 0;

    public void OnClick()
    {
        // 数値を一時出来にゲームマネージャーに送信する
        GameManager.SetAddParameter(parameterDelta);
    }
}
