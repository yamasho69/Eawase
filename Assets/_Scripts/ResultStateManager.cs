using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using DG.Tweening;

public class ResultStateManager : MonoBehaviour {

    // ���Ԃ�\������e�L�X�g
    public Text TimerText;

    /// <summary>
    /// �Q�[���Ōo�߂������Ԃ�\������
    /// </summary>
    public void SetTimerText(int timer) {
        this.TimerText.text = timer.ToString();
    }
}
