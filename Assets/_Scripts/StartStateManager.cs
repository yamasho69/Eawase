using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StartStateManager : MonoBehaviour {

    // �Q�[���̊J�n�e�L�X�g�̍��W
    public RectTransform GameStartTextRt;

    /// <summary>
    /// �e�L�X�g�̊g��A�j���[�V����
    /// </summary>
    public void EnlarAnimation() {

        this.GameStartTextRt.DOScale(Vector3.one * 1.5f, 0.5f)
            .OnComplete(() => {
                // �e�L�X�g�̏k���A�j���[�V����
                this.mShrinkAnimation();
            });
    }

    /// <summary>
    /// �e�L�X�g�̏k���A�j���[�V����
    /// </summary>
    private void mShrinkAnimation() {

        this.GameStartTextRt.DOScale(Vector3.one * 0.5f, 0.5f)
            .OnComplete(() => {
                // �e�L�X�g�̏k���A�j���[�V����
                this.EnlarAnimation();
            });
    }
}