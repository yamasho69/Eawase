using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;

public class Card : MonoBehaviour {
    // �J�[�h��ID
    public int Id;

    // �\������J�[�h�̉摜
    public Image CardImage;

    // ���ߏ����p
    public CanvasGroup CanGroup;

    // �I������Ă��邩����
    private bool mIsSelected = false;

    public bool IsSelected => this.mIsSelected;


    // �J�[�h���
    private CardData mData;

    // ���W���
    public RectTransform mRt;

    // �J�[�h�̐ݒ�
    public void Set(CardData data) {

        // �J�[�h����ݒ�
        this.mData = data;

        // ID��ݒ肷��
        this.Id = data.Id;

        // �\������摜��ݒ肷��
        // ����͑S�ė��ʕ\���Ƃ���
        this.CardImage.sprite = Resources.Load<Sprite>("Image/card_back");

        // �I�𔻒�t���O������������
        this.mIsSelected = false;

        // �A���t�@�l��1�ɐݒ�
        this.CanGroup.alpha = 1;

        // ���W�����擾���Ă���
        this.mRt = this.GetComponent<RectTransform>();

    }

    public void OnClick() {

        // �J�[�h���\�ʂɂȂ��Ă����ꍇ�͖���
        if (this.mIsSelected) {
            return;
        }

        Debug.Log("OnClick");

        // ��]�������s��
        this.onRotate(() => {
            // �I�𔻒�t���O��L���ɂ���
            this.mIsSelected = true;

            // �J�[�h��\�ʂɂ���
            this.CardImage.sprite = this.mData.ImgSprite;

            // Y���W�����ɖ߂�
            this.onReturnRotate(() => {
                // �I������CardId��ۑ����悤�I
                GameStateController.Instance.SelectedCardIdList.Add(this.mData.Id);
            });
        });
    }

    /// <summary>
    /// �J�[�h��90�x�ɉ�]����
    /// </summary>
    private void onRotate(Action onComp) {

        // 90�x��]����
        this.mRt.DORotate(new Vector3(0f, 90f, 0f), 0.2f)
            // ��]���I��������
            .OnComplete(() => {

                if (onComp != null) {
                    onComp();
                }
            });
    }

    /// <summary>
    /// �J�[�h�̉�]�������ɖ߂�
    /// </summary>
    private void onReturnRotate(Action onComp) {

        this.mRt.DORotate(new Vector3(0f, 0f, 0f), 0.2f)
            // ��]���I�������
            .OnComplete(() => {

                if (onComp != null) {
                    onComp();
                }
            });
    }

    ///  <summary>
    /// �J�[�h��w�ʕ\�L�ɂ���
    /// </summary>
    public void SetHide() {

        // 90�x��]����
        this.onRotate(() => {

            // �I�𔻒�t���O������������
            this.mIsSelected = false;

            // �J�[�h��w�ʕ\���ɂ���
            this.CardImage.sprite = Resources.Load<Sprite>("Image/card_back");

            // �p�x�����ɂ��ǂ�
            this.onReturnRotate(() => {
                Debug.Log("onhide");
            });
        });
    }

    /// <summary>
    /// �J�[�h���\���ɂ���
    /// </summary>
    public void SetInvisible() {

        // �I���ϐݒ�ɂ���
        this.mIsSelected = true;

        // �A���t�@�l��0�ɐݒ� (��\��)
        this.CanGroup.alpha = 0;

    }
}

/// <summary>
/// �J�[�h�̏��N���X
/// </summary>
public class CardData {

    // �J�[�hID
    public int Id { get; private set; }

    // �摜
    public Sprite ImgSprite { get; private set; }

    public CardData(int _id, Sprite _sprite) {
        this.Id = _id;
        this.ImgSprite = _sprite;
    }
}
/*

// �J�[�h��ID
public int Id;

// �\������J�[�h�̉摜
public Image CardImage;

// ���ߏ����p
public CanvasGroup CanGroup;

// �I������Ă��邩����
private bool mIsSelected = false;

public bool IsSelected => this.mIsSelected;

// �J�[�h���
private CardData mData;

// ���W���
public RectTransform mRt;

// �J�[�h�̐ݒ�
public void Set(CardData data) {

    // �J�[�h����ݒ�
    this.mData = data;

    // ID��ݒ肷��
    this.Id = data.Id;

    // �\������摜��ݒ肷��
    // ����͑S�ė��ʕ\���Ƃ���
    this.CardImage.sprite = Resources.Load<Sprite>("Image/card_back");

    // �I�𔻒�t���O������������
    this.mIsSelected = false;

    // �A���t�@�l��1�ɐݒ�
    this.CanGroup.alpha = 1;

    // ���W�����擾���Ă���
    this.mRt = this.GetComponent<RectTransform>();

}

public void OnClick() {

    // �J�[�h���\�ʂɂȂ��Ă����ꍇ�͖���
    if (this.mIsSelected) {
        return;
    }

    Debug.Log("OnClick");

    // ��]�������s��
    this.onRotate(() => {
        // �I�𔻒�t���O��L���ɂ���
        this.mIsSelected = true;

        // �J�[�h��\�ʂɂ���
        this.CardImage.sprite = this.mData.ImgSprite;

        // Y���W�����ɖ߂�
        this.onReturnRotate(() => {
            // �I������CardId��ۑ����悤�I
            GameStateController.Instance.SelectedCardIdList.Add(this.mData.Id);
        });
    });
}

/// <summary>
/// �J�[�h��90�x�ɉ�]����
/// </summary>
private void onRotate(Action onComp) {

    // 90�x��]����
    this.mRt.DORotate(new Vector3(0f, 90f, 0f), 0.2f)
        // ��]���I��������
        .OnComplete(() => {

            if (onComp != null) {
                onComp();
            }
        });
}

/// <summary>
/// �J�[�h�̉�]�������ɖ߂�
/// </summary>
private void onReturnRotate(Action onComp) {

    this.mRt.DORotate(new Vector3(0f, 0f, 0f), 0.2f)
        // ��]���I�������
        .OnComplete(() => {

            if (onComp != null) {
                onComp();
            }
        });
}

///  <summary>
/// �J�[�h��w�ʕ\�L�ɂ���
/// </summary>
public void SetHide() {

    // 90�x��]����
    this.onRotate(() => {

        // �I�𔻒�t���O������������
        this.mIsSelected = false;

        // �J�[�h��w�ʕ\���ɂ���
        this.CardImage.sprite = Resources.Load<Sprite>("Image/card_back");

        // �p�x�����ɂ��ǂ�
        this.onReturnRotate(() => {
            Debug.Log("onhide");
        });
    });
}

/// <summary>
/// �J�[�h���\���ɂ���
/// </summary>
public void SetInvisible() {

    // �I���ϐݒ�ɂ���
    this.mIsSelected = true;

    // �A���t�@�l��0�ɐݒ� (��\��)
    this.CanGroup.alpha = 0;

}
}

/// <summary>
/// �J�[�h�̏��N���X
/// </summary>
public class CardData {

// �J�[�hID
public int Id { get; private set; }

// �摜
public Sprite ImgSprite { get; private set; }

public CardData(int _id, Sprite _sprite) {
    this.Id = _id;
    this.ImgSprite = _sprite;
}
}*/
