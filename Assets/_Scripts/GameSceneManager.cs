using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using NCMB;

public class GameSceneManager : MonoBehaviour {

    // 一致したカードIDリスト
    private List<int> mContainCardIdList = new List<int>();

    // カード生成マネージャクラス
    public CardCreateManager CardCreate;

    // 時間管理クラス
    public TimerManager timerManager;

    //結果表示クラス
    public ResultStateManager resultStateManager;

    // スタートステートクラス
    public StartStateManager startStateManager;

    // 経過時間
    private float mElapsedTime;

    // ゲームステート管理
    private EGameState mEGameState;

    //タイマー
    [Header("タイマー")]public GameObject Timer;

    //カードエリア
    [Header("カードエリア")] public GameObject CardArea;

    //配置するカードの種類
    [Header("配置するカードの種類")]public int cardKinds;

    private CardCreateManager cardCreateManager;

    [Header("正解音")] public AudioClip sound1;
    AudioSource audioSource;

    void Start() {
        // ゲームステートを初期化
        this.mEGameState = EGameState.START;

        // スタートエリアを表示
        this.startStateManager.gameObject.SetActive(false);

        // ゲームのステート管理
        this.mSetGameState();

        GameObject gameObject = GameObject.Find("GameManager");
        cardCreateManager = gameObject.GetComponent<CardCreateManager>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {

        // GameState が GAME状態なら
        if (this.mEGameState == EGameState.GAME) {
            this.mElapsedTime += Time.deltaTime;

            this.timerManager.SetText(mElapsedTime);

            // 選択したカードが２枚以上になったら
            if (GameStateController.Instance.SelectedCardIdList.Count >= 2) {

                // 最初に選択したCardIDを取得する
                int selectedId = GameStateController.Instance.SelectedCardIdList[0];

                // 2枚目にあったカードと一緒だったら
                if (selectedId == GameStateController.Instance.SelectedCardIdList[1]) {

                    Debug.Log($"Contains! {selectedId}");
                    // 一致したカードIDを保存する
                    this.mContainCardIdList.Add(selectedId);
                    audioSource.PlayOneShot(sound1);
                }

                // カードの表示切り替えを行う
                this.CardCreate.HideCardList(this.mContainCardIdList);

                // 選択したカードリストを初期化する
                GameStateController.Instance.SelectedCardIdList.Clear();
            }

            // 配置した全種類のカードを獲得したら
            if (this.mContainCardIdList.Count >= cardKinds) {

                // ゲームをリザルトステートに遷移する
                this.mEGameState = EGameState.RESULT;
                this.mSetGameState();
            }
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
    }

    /// <summary>
    /// ゲームステートで処理を変更する
    /// </summary>
    private void mSetGameState() {

        switch (this.mEGameState) {
            // スタート画面
            case EGameState.START:
                // スタートエリアを表示
                this.startStateManager.gameObject.SetActive(true);
                // ゲームスタートの開始
                this.mSetStartState();
                break;
            // ゲーム準備期間
            case EGameState.READY:
                this.startStateManager.gameObject.SetActive(false);
                this.CardArea.gameObject.SetActive(true);
                // ゲームの準備ステートを開始する
                this.mSetGameReady();
                break;
            // ゲーム中
            case EGameState.GAME:
                cardCreateManager.canTurnCard = 2;
                Timer.SetActive(true);//タイマーを表示
                break;
            // 結果画面
            case EGameState.RESULT:
                this.CardArea.gameObject.SetActive(false);
                this.resultStateManager.gameObject.SetActive(true);
                Timer.SetActive(false);//タイマーを非表示
                this.mSetResultState();
                break;
        }
    }
    /// <summary>
    /// リザルトステートの設定処理
    /// </summary>
    private void mSetResultState() {

        this.resultStateManager.SetTimerText(mElapsedTime);

        String clearTime1 = mElapsedTime.ToString("f2");//小数点2桁まで
        float clearTime2 = float.Parse(clearTime1);

        // Type == Number の場合
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(clearTime2);
    }

    /// <summary>
    /// スタート画面に遷移する
    /// </summary>
    public void OnBackStartState() {
        // 時間を初期化
        this.mElapsedTime = 0f;

        // ResultAreaを非表示にする
        this.resultStateManager.gameObject.SetActive(false);

        // ゲームステートをStartに変更
        this.mEGameState = EGameState.START;

        // ゲームのステート管理
        this.mSetGameState();
    }

    private void mSetStartState() {
        // テキストの拡大縮小アニメーション
        this.startStateManager.EnlarAnimation();
    }

    /// <summary>
    /// Readyステートに遷移する
    /// </summary>
    public void OnGameStart() {
        // ゲームステートを初期化
        this.mEGameState = EGameState.READY;

        // ゲームのステート管理
        this.mSetGameState();
    }
}
