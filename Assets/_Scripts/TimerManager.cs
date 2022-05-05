using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour {

    // 時間表示
    public Text TimerText;

    /// <summary>
    /// タイマーのテキストの設定
    /// </summary>
    public void SetText(float time) {

        this.TimerText.text = "Time : " + time.ToString("f2");//小数点2桁まで
    }
}