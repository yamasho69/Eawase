using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;

public class Card : MonoBehaviour {
    // カードのID
    public int Id;

    // 表示するカードの画像
    public Image CardImage;

    // 透過処理用
    public CanvasGroup CanGroup;

    // 選択されているか判定
    private bool mIsSelected = false;

    public bool IsSelected => this.mIsSelected;

    // カード情報
    private CardData mData;

    // 座標情報
    public RectTransform mRt;

    private Tween tween;

    private CardCreateManager cardCreateManager;

    [Header("めくる音")]public AudioClip sound1;
    AudioSource audioSource;

    private void Start() {
        GameObject gameObject = GameObject.Find("GameManager");
        cardCreateManager = gameObject.GetComponent<CardCreateManager>();
        audioSource = GetComponent<AudioSource>();
    }

    // カードの設定
    public void Set(CardData data) {

        // カード情報を設定
        this.mData = data;

        // IDを設定する
        this.Id = data.Id;

        // 表示する画像を設定する
        // 初回は全て裏面表示とする
        this.CardImage.sprite = Resources.Load<Sprite>("Image/card_back");

        // 選択判定フラグを初期化する
        this.mIsSelected = false;

        // アルファ値を1に設定
        this.CanGroup.alpha = 1;

        // 座標情報を取得しておく
        this.mRt = this.GetComponent<RectTransform>();
    }

    public void OnClick() {

        // カードが表面になっていた場合は無効
        if (this.mIsSelected || cardCreateManager.canTurnCard<=0) {
            return;
        }

        Debug.Log("OnClick");
        audioSource.PlayOneShot(sound1);
        // 回転処理を行う
        this.onRotate(() => {
            // 選択判定フラグを有効にする
            this.mIsSelected = true;

            // カードを表面にする
            this.CardImage.sprite = this.mData.ImgSprite;

            // Y座標を元に戻す
            this.onReturnRotate(() => {
                // 選択したCardIdを保存しよう！
                GameStateController.Instance.SelectedCardIdList.Add(this.mData.Id);
            });
            cardCreateManager.canTurnCard--;
        });
    }

    /// <summary>
    /// カードを90度に回転する
    /// </summary>
    private void onRotate(Action onComp) {

        // 90度回転する
        this.mRt.DORotate(new Vector3(0f, 90f, 0f), 0.2f)
            // 回転が終了したら
            .OnComplete(() => {

                if (onComp != null) {
                    onComp();
                }
            });
    }

    /// <summary>
    /// カードの回転軸を元に戻す
    /// </summary>
    private void onReturnRotate(Action onComp) {

        this.mRt.DORotate(new Vector3(0f, 0f, 0f), 0.2f)
            // 回転が終わったら
            .OnComplete(() => {

                if (onComp != null) {
                    onComp();
                }
            });
    }

    ///  <summary>
    /// カードを背面表記にする
    /// </summary>
     public void SetHide() {
        cardCreateManager.canTurnCard = 0;//数がずれることがあるので2枚めくった時点で0にする。
        Invoke("SetHide2", 1.0f);
    }

    //1.0fめくった後に表示するために関数を分けた
    public void SetHide2() {
        // 90度回転する
        this.onRotate(() => {

            // 選択判定フラグを初期化する
            this.mIsSelected = false;

            // カードを背面表示にする
            this.CardImage.sprite = Resources.Load<Sprite>("Image/card_back");

            // 角度を元にもどす
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
    /// カードを非表示にする
    /// </summary>
    public void SetInvisible() {
        cardCreateManager.canTurnCard = 0;//数がずれることがあるので2枚めくった時点で0にする。
        Invoke("SetInvisible2", 0.5f);
    }

    //0.5fめくった後に表示するために関数を分けた
    public void SetInvisible2() {

        // 選択済設定にする
        this.mIsSelected = true;

        // アルファ値を0に設定 (非表示)
        this.CanGroup.alpha = 0;

        Invoke("SetInvisible3", 0.5f);
    }

    public void SetInvisible3() {
        cardCreateManager.canTurnCard = 2;
    }

    private void OnDisable() {
        // Tween破棄
        if (DOTween.instance != null) {
            tween?.Kill();
        }
    }

}

/// <summary>
/// カードの情報クラス
/// </summary>
public class CardData {

    // カードID
    public int Id { get; private set; }

    // 画像
    public Sprite ImgSprite { get; private set; }

    public CardData(int _id, Sprite _sprite) {
        this.Id = _id;
        this.ImgSprite = _sprite;
    }
}
