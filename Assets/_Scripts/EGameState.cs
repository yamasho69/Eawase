using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using DG.Tweening;

/// <summary>
/// ゲームの進行ステート
/// </summary>
public enum EGameState {

    START = 0,
    READY = 1,
    GAME = 2,
    RESULT = 3
}
