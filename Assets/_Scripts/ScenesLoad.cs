using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;

public class ScenesLoad : MonoBehaviour
{
    public GameObject manager;
    public void OnClick()
    {
        DOTween.Clear(true);//超重要！シーン切り替え前にやらないとエラーがでる！https://wakky.tech/unity-dotween-scene-load/
        SceneManager.LoadScene("Game");
    }
}
