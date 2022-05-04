using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using DG.Tweening;

public class GameSceneManager : MonoBehaviour {

    // ��v�����J�[�hID���X�g
    private List<int> mContainCardIdList = new List<int>();

    // �J�[�h�����}�l�[�W���N���X
    public CardCreateManager CardCreate;

    // ���ԊǗ��N���X
    public TimerManager timerManager;

    // �o�ߎ���
    private float mElapsedTime;

    // �Q�[���X�e�[�g�Ǘ�
    private EGameState mEGameState;

    void Start() {
        // �Q�[���X�e�[�g��������
        this.mEGameState = EGameState.READY;

        // �Q�[���̃X�e�[�g�Ǘ�
        this.mSetGameState();
    }

    void Update() {

        // GameState �� GAME��ԂȂ�
        if (this.mEGameState == EGameState.GAME) {
            this.mElapsedTime += Time.deltaTime;

            this.timerManager.SetText((int)this.mElapsedTime);

            // �I�������J�[�h���Q���ȏ�ɂȂ�����
            if (GameStateController.Instance.SelectedCardIdList.Count >= 2) {

                // �ŏ��ɑI������CardID���擾����
                int selectedId = GameStateController.Instance.SelectedCardIdList[0];

                // 2���ڂɂ������J�[�h�ƈꏏ��������
                if (selectedId == GameStateController.Instance.SelectedCardIdList[1]) {

                    Debug.Log($"Contains! {selectedId}");
                    // ��v�����J�[�hID��ۑ�����
                    this.mContainCardIdList.Add(selectedId);
                }

                // �J�[�h�̕\���؂�ւ����s��
                this.CardCreate.HideCardList(this.mContainCardIdList);

                // �I�������J�[�h���X�g������������
                GameStateController.Instance.SelectedCardIdList.Clear();
            }
        }
    }

    /// <summary>
    /// �Q�[���̏����X�e�[�g���J�n����
    /// </summary>
    private void mSetGameReady() {

        // �J�[�h�z�z�A�j���[�V�������I��������̃R�[���o�b�N��������������
        this.CardCreate.OnCardAnimeComp = null;
        this.CardCreate.OnCardAnimeComp = () => {

            // �Q�[���X�e�[�g��GAME��ԂɕύX����
            this.mEGameState = EGameState.GAME;
            this.mSetGameState();
        };

        // ��v�����J�[�hID���X�g��������
        this.mContainCardIdList.Clear();

        // �J�[�h���X�g�𐶐�����
        this.CardCreate.CreateCard();

        // ���Ԃ�������
        this.mElapsedTime = 0f;
    }

    /// <summary>
    /// �Q�[���X�e�[�g�ŏ�����ύX����
    /// </summary>
    private void mSetGameState() {

        switch (this.mEGameState) {
            // �X�^�[�g���
            case EGameState.START:
                break;
            // �Q�[����������
            case EGameState.READY:
                // �Q�[���̏����X�e�[�g���J�n����
                this.mSetGameReady();
                break;
            // �Q�[����
            case EGameState.GAME:
                break;
            // ���ʉ��
            case EGameState.RESULT:
                break;
        }
    }
}
/*
// ��v�����J�[�hID���X�g
private List<int> mContainCardIdList = new List<int>();

// �J�[�h�����}�l�[�W���N���X
public CardCreateManager CardCreate;

// ���ԊǗ��N���X
public TimerManager timerManager;

// �o�ߎ���
private float mElapsedTime;

// �Q�[���X�e�[�g�Ǘ�
private EGameState mEGameState;
void Start() {

    // �Q�[���X�e�[�g��������
    this.mEGameState = EGameState.READY;

    // �Q�[���̃X�e�[�g�Ǘ�
    this.mSetGameState();
}

void Update() {

    // GameState �� GAME��ԂȂ�
    if (this.mEGameState == EGameState.GAME) {
        this.mElapsedTime += Time.deltaTime;

        this.timerManager.SetText((int)this.mElapsedTime);

        // �I�������J�[�h���Q���ȏ�ɂȂ�����
        if (GameStateController.Instance.SelectedCardIdList.Count >= 2) {

            // �ŏ��ɑI������CardID���擾����
            int selectedId = GameStateController.Instance.SelectedCardIdList[0];

            // 2���ڂɂ������J�[�h�ƈꏏ��������
            if (selectedId == GameStateController.Instance.SelectedCardIdList[1]) {

                Debug.Log($"Contains! {selectedId}");
                // ��v�����J�[�hID��ۑ�����
                this.mContainCardIdList.Add(selectedId);
            }

            // �J�[�h�̕\���؂�ւ����s��
            this.CardCreate.HideCardList(this.mContainCardIdList);

            // �I�������J�[�h���X�g������������
            GameStateController.Instance.SelectedCardIdList.Clear();
        }
    }

}

/// <summary>
/// �Q�[���X�e�[�g�ŏ�����ύX����
/// </summary>
private void mSetGameState() {

    switch (this.mEGameState) {
        // �X�^�[�g���
        case EGameState.START:
            break;
        // �Q�[����������
        case EGameState.READY:
            // �Q�[���̏����X�e�[�g���J�n����
            this.mSetGameReady();
            break;
        // �Q�[����
        case EGameState.GAME:
            break;
        // ���ʉ��
        case EGameState.RESULT:
            break;
    }
}

/// <summary>
/// �Q�[���̏����X�e�[�g���J�n����
/// </summary>
private void mSetGameReady() {

    // �J�[�h�z�z�A�j���[�V�������I��������̃R�[���o�b�N��������������
    this.CardCreate.OnCardAnimeComp = null;
    this.CardCreate.OnCardAnimeComp = () => {

        // �Q�[���X�e�[�g��GAME��ԂɕύX����
        this.mEGameState = EGameState.GAME;
        this.mSetGameState();
    };

    // ��v�����J�[�hID���X�g��������
    this.mContainCardIdList.Clear();

    // �J�[�h���X�g�𐶐�����
    this.CardCreate.CreateCard();

    // ���Ԃ�������
    this.mElapsedTime = 0f;
}
}*/
