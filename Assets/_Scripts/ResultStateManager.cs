using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using DG.Tweening;

public class ResultStateManager : MonoBehaviour {

    // 時間を表示するテキスト
    public Text TimerText;

    /// <summary>
    /// ゲームで経過した時間を表示する
    /// </summary>
    public void SetTimerText(float timer) {
        this.TimerText.text = timer.ToString("f2");//小数点2桁まで
    }
}
