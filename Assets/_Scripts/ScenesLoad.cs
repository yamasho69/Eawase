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
        DOTween.Clear(true);//���d�v�I�V�[���؂�ւ��O�ɂ��Ȃ��ƃG���[���ł�Ihttps://wakky.tech/unity-dotween-scene-load/
        SceneManager.LoadScene("Game");
    }
}
