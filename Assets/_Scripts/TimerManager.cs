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
    public void SetText(float time) {

        this.TimerText.text = "Time : " + time.ToString("f2");//�����_2���܂�
    }
}