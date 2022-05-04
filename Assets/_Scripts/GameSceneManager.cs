using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using DG.Tweening;

public class GameSceneManager : MonoBehaviour {

    // 一致したカードIDリスト
    private List<int> mContainCardIdList = new List<int>();

    // カード生成マネージャクラス
    public CardCreateManager CardCreate;

    void Start() {

        // 一致したカードIDリストを初期化
        this.mContainCardIdList.Clear();

        // カードリストを生成する
        this.CardCreate.CreateCard();
    }

    void Update() {

        // 選択したカードが２枚以上になったら
        if (GameStateController.Instance.SelectedCardIdList.Count >= 2) {

            // 最初に選択したCardIDを取得する
            int selectedId = GameStateController.Instance.SelectedCardIdList[0];

            // 2枚目にあったカードと一緒だったら
            if (selectedId == GameStateController.Instance.SelectedCardIdList[1]) {

                Debug.Log($"Contains! {selectedId}");
                // 一致したカードIDを保存する
                this.mContainCardIdList.Add(selectedId);
            }

            // カードの表示切り替えを行う
            this.CardCreate.HideCardList(this.mContainCardIdList);

            // 選択したカードリストを初期化する
            GameStateController.Instance.SelectedCardIdList.Clear();
        }
    }
}
/*
// 一致したカードIDリスト
private List<int> mContainCardIdList = new List<int>();

// カード生成マネージャクラス
public CardCreateManager CardCreate;

// 時間管理クラス
public TimerManager timerManager;

// 経過時間
private float mElapsedTime;

// ゲームステート管理
private EGameState mEGameState;
void Start() {

    // ゲームステートを初期化
    this.mEGameState = EGameState.READY;

    // ゲームのステート管理
    this.mSetGameState();
}

void Update() {

    // GameState が GAME状態なら
    if (this.mEGameState == EGameState.GAME) {
        this.mElapsedTime += Time.deltaTime;

        this.timerManager.SetText((int)this.mElapsedTime);

        // 選択したカードが２枚以上になったら
        if (GameStateController.Instance.SelectedCardIdList.Count >= 2) {

            // 最初に選択したCardIDを取得する
            int selectedId = GameStateController.Instance.SelectedCardIdList[0];

            // 2枚目にあったカードと一緒だったら
            if (selectedId == GameStateController.Instance.SelectedCardIdList[1]) {

                Debug.Log($"Contains! {selectedId}");
                // 一致したカードIDを保存する
                this.mContainCardIdList.Add(selectedId);
            }

            // カードの表示切り替えを行う
            this.CardCreate.HideCardList(this.mContainCardIdList);

            // 選択したカードリストを初期化する
            GameStateController.Instance.SelectedCardIdList.Clear();
        }
    }

}

/// <summary>
/// ゲームステートで処理を変更する
/// </summary>
private void mSetGameState() {

    switch (this.mEGameState) {
        // スタート画面
        case EGameState.START:
            break;
        // ゲーム準備期間
        case EGameState.READY:
            // ゲームの準備ステートを開始する
            this.mSetGameReady();
            break;
        // ゲーム中
        case EGameState.GAME:
            break;
        // 結果画面
        case EGameState.RESULT:
            break;
    }
}

/// <summary>
/// ゲームの準備ステートを開始する
/// </summary>
private void mSetGameReady() {

    // カード配布アニメーションが終了した後のコールバック処理を実装する
    this.CardCreate.OnCardAnimeComp = null;
    this.CardCreate.OnCardAnimeComp = () => {

        // ゲームステートをGAME状態に変更する
        this.mEGameState = EGameState.GAME;
        this.mSetGameState();
    };

    // 一致したカードIDリストを初期化
    this.mContainCardIdList.Clear();

    // カードリストを生成する
    this.CardCreate.CreateCard();

    // 時間を初期化
    this.mElapsedTime = 0f;
}
}*/
