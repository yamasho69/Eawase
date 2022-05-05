using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StartStateManager : MonoBehaviour {

    // �Q�[���̊J�n�e�L�X�g�̍��W
    public RectTransform GameStartTextRt;
    private Tween tween;

    /// <summary>
    /// �e�L�X�g�̊g��A�j���[�V����
    /// </summary>
    public void EnlarAnimation() {

        this.GameStartTextRt.DOScale(Vector3.one * 1.1f, 0.5f)
            .OnComplete(() => {
                // �e�L�X�g�̏k���A�j���[�V����
                this.mShrinkAnimation();
            });
    }

    /// <summary>
    /// �e�L�X�g�̏k���A�j���[�V����
    /// </summary>
    private void mShrinkAnimation() {

        this.GameStartTextRt.DOScale(Vector3.one * 0.9f, 0.5f)
            .OnComplete(() => {
                // �e�L�X�g�̊g��A�j���[�V����
                this.EnlarAnimation();
            });
    }

    private void OnDisable() {
        // Tween�j��
        if (DOTween.instance != null) {
            tween?.Kill();
        }
    }
}