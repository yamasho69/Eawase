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

    private Tween tween;

    private CardCreateManager cardCreateManager;

    [Header("�߂��鉹")]public AudioClip sound1;
    AudioSource audioSource;

    private void Start() {
        GameObject gameObject = GameObject.Find("GameManager");
        cardCreateManager = gameObject.GetComponent<CardCreateManager>();
        audioSource = GetComponent<AudioSource>();
    }

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
        if (this.mIsSelected || cardCreateManager.canTurnCard<=0) {
            return;
        }

        Debug.Log("OnClick");
        audioSource.PlayOneShot(sound1);
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
            cardCreateManager.canTurnCard--;
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
        cardCreateManager.canTurnCard = 0;//��������邱�Ƃ�����̂�2���߂��������_��0�ɂ���B
        Invoke("SetHide2", 1.0f);
    }

    //1.0f�߂�������ɕ\�����邽�߂Ɋ֐��𕪂���
    public void SetHide2() {
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
        Invoke("SetHide3", 0.5f);
    }

    public void SetHide3() {
        cardCreateManager.canTurnCard = 2;
    }

    /// <summary>
    /// �J�[�h���\���ɂ���
    /// </summary>
    public void SetInvisible() {
        cardCreateManager.canTurnCard = 0;//��������邱�Ƃ�����̂�2���߂��������_��0�ɂ���B
        Invoke("SetInvisible2", 0.5f);
    }

    //0.5f�߂�������ɕ\�����邽�߂Ɋ֐��𕪂���
    public void SetInvisible2() {

        // �I���ϐݒ�ɂ���
        this.mIsSelected = true;

        // �A���t�@�l��0�ɐݒ� (��\��)
        this.CanGroup.alpha = 0;

        Invoke("SetInvisible3", 0.5f);
    }

    public void SetInvisible3() {
        cardCreateManager.canTurnCard = 2;
    }

    private void OnDisable() {
        // Tween�j��
        if (DOTween.instance != null) {
            tween?.Kill();
        }
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
