using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;

public class CardCreateManager : MonoBehaviour {
    // ��������Card�I�u�W�F�N�g
    public Card CardPrefab;

    // �u�J�[�h�v�𐶐�����e�I�u�W�F�N�g
    public RectTransform CardCreateParent;

    // ���������J�[�h�I�u�W�F�N�g��ۑ�����
    public List<Card> CardList = new List<Card>();

    // �J�[�h���̏��ʂ������_���ɕύX�������X�g
    private List<CardData> mRandomCardDataList = new List<CardData>();

    // GridLayoutGroup
    public GridLayoutGroup GridLayout;

    // �J�[�h�̐����A�j���[�V�������I�������
    public Action OnCardAnimeComp;

    // �J�[�h�z��̃C���f�b�N�X
    private int mIndex;

    // �J�[�h�𐶐����鎞�̍����C���f�b�N�X
    private int mHelgthIdx;
    // �J�[�h�𐶐����鎞�̕��C���f�b�N�X
    private int mWidthIdx;

    // �J�[�h�̐����A�j���[�V�����̃A�j���[�V��������
    private readonly float DEAL_CAED_TIME = 0.1f;

    void Start() {
       
    }

    /// <summary>
    /// �J�[�h�𐶐�����
    /// </summary>
    public void CreateCard() {
        // �J�[�h��񃊃X�g
        List<CardData> cardDataList = new List<CardData>();

        // �\������J�[�h�摜���̃��X�g
        List<Sprite> imgList = new List<Sprite>();

        // Resources/Image�t�H���_���ɂ���摜���擾����
        imgList.Add(Resources.Load<Sprite>("Image/0"));
        imgList.Add(Resources.Load<Sprite>("Image/1"));
        imgList.Add(Resources.Load<Sprite>("Image/2"));
        imgList.Add(Resources.Load<Sprite>("Image/3"));
        imgList.Add(Resources.Load<Sprite>("Image/4"));
        imgList.Add(Resources.Load<Sprite>("Image/5"));
        imgList.Add(Resources.Load<Sprite>("Image/6"));
        imgList.Add(Resources.Load<Sprite>("Image/7"));
        imgList.Add(Resources.Load<Sprite>("Image/8"));
        imgList.Add(Resources.Load<Sprite>("Image/9"));

        // for���񂷉񐔂��擾����
        int loopCnt = imgList.Count;

        for (int i = 0; i < loopCnt; i++) {

            // �J�[�h���𐶐�����
            CardData carddata = new CardData(i, imgList[i]);
            cardDataList.Add(carddata);
        }
        this.mIndex = 0;
        this.mHelgthIdx = 0;
        this.mWidthIdx = 0;

        // ���������J�[�h���X�g�Q���̃��X�g�𐶐�����
        List<CardData> SumCardDataList = new List<CardData>();
        SumCardDataList.AddRange(cardDataList);

        // �����_�����X�g�̏�����
        this.mRandomCardDataList.Clear();

        // ���X�g�̒��g�������_���ɍĔz�u����
        this.mRandomCardDataList = SumCardDataList.OrderBy(a => Guid.NewGuid()).ToList();
        this.mRandomCardDataList.AddRange(SumCardDataList.OrderBy(a => Guid.NewGuid()).ToList());

        // GridLayout�𖳌�
        this.GridLayout.enabled = false;

        // �J�[�h��z��A�j���[�V��������
        this.mSetDealCardAnime();
        /*
        // �J�[�h�I�u�W�F�N�g�𐶐�����
        foreach (var _cardData in mRandomCardDataList) {

            // Instantiate �� Card�I�u�W�F�N�g�𐶐�
            Card card = Instantiate<Card>(this.CardPrefab, this.CardCreateParent);
            // �f�[�^��ݒ肷��
            card.Set(_cardData);

            // ���������J�[�h�I�u�W�F�N�g��ۑ�����
            this.CardList.Add(card);
        }*/
    }
    // <summary>
    /// �擾���Ă��Ȃ��J�[�h��w�ʂɂ���
    /// </summary>
    public void HideCardList(List<int> containCardIdList) {

        foreach (var _card in this.CardList) {

            // ���Ɋl�������J�[�hID�̏ꍇ�A��\���ɂ���
            if (containCardIdList.Contains(_card.Id)) {

                // �J�[�h���\���ɂ���
                _card.SetInvisible();
            }
            // �J�[�h���\�� && �l�����Ă��Ȃ��J�[�h�͗��ʕ\���ɂ���
            else if (_card.IsSelected) {

                // �J�[�h�𗠖ʕ\���ɂ���
                _card.SetHide();
            }
        }
    }

    /// <summary>
    /// �J�[�h��z��A�j���[�V��������
    /// </summary>
    private void mSetDealCardAnime() {

        var _cardData = this.mRandomCardDataList[this.mIndex];

        // Instantiate �� Card�I�u�W�F�N�g�𐶐�
        Card card = Instantiate<Card>(this.CardPrefab, this.CardCreateParent);
        // �f�[�^��ݒ肷��
        card.Set(_cardData);
        // �J�[�h�̏����l��ݒ� (��ʊO�ɂ���)
        card.mRt.anchoredPosition = new Vector2(1900, 0f);
        // �T�C�Y��GridLayout��CellSize�ɐݒ�
        card.mRt.sizeDelta = this.GridLayout.cellSize;

        // �J�[�h�̈ړ����ݒ�
        float posX = (this.GridLayout.cellSize.x * this.mWidthIdx) + (this.GridLayout.spacing.x * (this.mWidthIdx + 1));
        float posY = ((this.GridLayout.cellSize.y * this.mHelgthIdx) + (this.GridLayout.spacing.y * this.mHelgthIdx)) * -1f;

        // DOAnchorPos�ŃA�j���[�V�������s��
        card.mRt.DOAnchorPos(new Vector2(posX, posY), this.DEAL_CAED_TIME)
            // �A�j���[�V�������I��������
            .OnComplete(() => {
                // ���������J�[�h�I�u�W�F�N�g��ۑ�����
                this.CardList.Add(card);

                // ��������J�[�h�f�[�^���X�g�̃C���f�b�N�X���X�V
                this.mIndex++;
                this.mWidthIdx++;

                // �����C���f�b�N�X�����X�g�̍ő�l���}������
                if (this.mIndex >= this.mRandomCardDataList.Count) {
                    // GridLayout��L���ɂ��A�����������I������
                    this.GridLayout.enabled = true;
                    // �A�j���[�V�����I�����̊֐���錾����
                    if (this.OnCardAnimeComp != null) {
                        this.OnCardAnimeComp();
                    }
                } else {
                    // GridLayout�̐܂�Ԃ��n�_�ɗ�����
                    if (this.mIndex % this.GridLayout.constraintCount == 0) {
                        // �����̐����ӏ����X�V
                        this.mHelgthIdx++;
                        this.mWidthIdx = 0;
                    }
                    // �A�j���[�V�����������ċA��������
                    this.mSetDealCardAnime();
                }
            });
    }
}
/*
    // ��������Card�I�u�W�F�N�g
    public Card CardPrefab;

    // �u�J�[�h�v�𐶐�����e�I�u�W�F�N�g
    public RectTransform CardCreateParent;

    // ���������J�[�h�I�u�W�F�N�g��ۑ�����
    public List<Card> CardList = new List<Card>();

    // �J�[�h���̏��ʂ������_���ɕύX�������X�g
    private List<CardData> mRandomCardDataList = new List<CardData>();

    // GridLayoutGroup
    public GridLayoutGroup GridLayout;

    // �J�[�h�̐����A�j���[�V�������I�������
    public Action OnCardAnimeComp;

    // �J�[�h�z��̃C���f�b�N�X
    private int mIndex;

    // �J�[�h�𐶐����鎞�̍����C���f�b�N�X
    private int mHelgthIdx;
    // �J�[�h�𐶐����鎞�̕��C���f�b�N�X
    private int mWidthIdx;

    // �J�[�h�̐����A�j���[�V�����̃A�j���[�V��������
    private readonly float DEAL_CAED_TIME = 1f;
    void Start() {
        //Card card = Instantiate<Card>(this.CardPrefab, this.CardCreateParent);
       
    }

    /// <summary>
    /// �J�[�h�𐶐�����
    /// </summary>
    public void CreateCard() {
        // �J�[�h��񃊃X�g
        List<CardData> cardDataList = new List<CardData>();

        // �\������J�[�h�摜���̃��X�g
        List<Sprite> imgList = new List<Sprite>();

        // Resources/Image�t�H���_���ɂ���摜���擾����
        imgList.Add(Resources.Load<Sprite>("Image/0"));
        imgList.Add(Resources.Load<Sprite>("Image/1"));
        imgList.Add(Resources.Load<Sprite>("Image/2"));
        imgList.Add(Resources.Load<Sprite>("Image/3"));
        imgList.Add(Resources.Load<Sprite>("Image/4"));
        imgList.Add(Resources.Load<Sprite>("Image/5"));
        imgList.Add(Resources.Load<Sprite>("Image/6"));
        imgList.Add(Resources.Load<Sprite>("Image/7"));
        imgList.Add(Resources.Load<Sprite>("Image/8"));
        imgList.Add(Resources.Load<Sprite>("Image/9"));

        // for���񂷉񐔂��擾����
        int loopCnt = imgList.Count;

        for (int i = 0; i < loopCnt; i++) {
            // �J�[�h���𐶐�����
            CardData cardata = new CardData(i, imgList[i]);
            cardDataList.Add(cardata);
        }

        this.mIndex = 0;
        this.mHelgthIdx = 0;
        this.mWidthIdx = 0;

        // ���������J�[�h���X�g�Q���̃��X�g�𐶐�����
        List<CardData> SumCardDataList = new List<CardData>();
        SumCardDataList.AddRange(cardDataList);

        // �����_�����X�g�̏�����
        this.mRandomCardDataList.Clear();

        // ���X�g�̒��g�������_���ɍĔz�u����
        this.mRandomCardDataList = SumCardDataList.OrderBy(a => Guid.NewGuid()).ToList();
        this.mRandomCardDataList.AddRange(SumCardDataList.OrderBy(a => Guid.NewGuid()).ToList());

        // GridLayout�𖳌�
        this.GridLayout.enabled = false;

        // �J�[�h��z��A�j���[�V��������
        this.mSetDealCardAnime();

        // �J�[�h�I�u�W�F�N�g�𐶐�����
        foreach (var _cardData in mRandomCardDataList) {

            // Instantiate �� Card�I�u�W�F�N�g�𐶐�
            Card card = Instantiate<Card>(this.CardPrefab, this.CardCreateParent);
            // �f�[�^��ݒ肷��
            card.Set(_cardData);

            // ���������J�[�h�I�u�W�F�N�g��ۑ�����
            this.CardList.Add(card);
        }
    }

    // <summary>
    /// �擾���Ă��Ȃ��J�[�h��w�ʂɂ���
    /// </summary>
    public void HideCardList(List<int> containCardIdList) {

        foreach (var _card in this.CardList) {

            // ���Ɋl�������J�[�hID�̏ꍇ�A��\���ɂ���
            if (containCardIdList.Contains(_card.Id)) {

                // �J�[�h���\���ɂ���
                _card.SetInvisible();
            }
            // �J�[�h���\�� && �l�����Ă��Ȃ��J�[�h�͗��ʕ\���ɂ���
            else if (_card.IsSelected) {

                // �J�[�h�𗠖ʕ\���ɂ���
                _card.SetHide();
            }
        }
    }

    /// <summary>
    /// �J�[�h��z��A�j���[�V��������
    /// </summary>
    private void mSetDealCardAnime() {

        var _cardData = this.mRandomCardDataList[this.mIndex];

        // Instantiate �� Card�I�u�W�F�N�g�𐶐�
        Card card = Instantiate<Card>(this.CardPrefab, this.CardCreateParent);
        // �f�[�^��ݒ肷��
        card.Set(_cardData);
        // �J�[�h�̏����l��ݒ� (��ʊO�ɂ���)
        card.mRt.anchoredPosition = new Vector2(1900, 0f);
        // �T�C�Y��GridLayout��CellSize�ɐݒ�
        card.mRt.sizeDelta = this.GridLayout.cellSize;

        // �J�[�h�̈ړ����ݒ�
        float posX = (this.GridLayout.cellSize.x * this.mWidthIdx) + (this.GridLayout.spacing.x * (this.mWidthIdx + 1));
        float posY = ((this.GridLayout.cellSize.y * this.mHelgthIdx) + (this.GridLayout.spacing.y * this.mHelgthIdx)) * -1f;

        // DOAnchorPos�ŃA�j���[�V�������s��
        card.mRt.DOAnchorPos(new Vector2(posX, posY), this.DEAL_CAED_TIME)
            // �A�j���[�V�������I��������
            .OnComplete(() => {
                // ���������J�[�h�I�u�W�F�N�g��ۑ�����
                this.CardList.Add(card);

                // ��������J�[�h�f�[�^���X�g�̃C���f�b�N�X���X�V
                this.mIndex++;
                this.mWidthIdx++;

                // �����C���f�b�N�X�����X�g�̍ő�l���}������
                if (this.mIndex >= this.mRandomCardDataList.Count) {
                    // GridLayout��L���ɂ��A�����������I������
                    this.GridLayout.enabled = true;
                } else {
                    // GridLayout�̐܂�Ԃ��n�_�ɗ�����
                    if (this.mIndex % this.GridLayout.constraintCount == 0) {
                        // �����̐����ӏ����X�V
                        this.mHelgthIdx++;
                        this.mWidthIdx = 0;
                    }
                    // �A�j���[�V�����������ċA��������
                    this.mSetDealCardAnime();
                }
            });
    }
*/
