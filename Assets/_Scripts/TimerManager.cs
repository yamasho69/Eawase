using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour {

    // ���ԕ\��
    public Text TimerText;

    /// <summary>
    /// �^�C�}�[�̃e�L�X�g�̐ݒ�
    /// </summary>
    public void SetText(int time) {

        this.TimerText.text = "Time : " + time;
    }
}