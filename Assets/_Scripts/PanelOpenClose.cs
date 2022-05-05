using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PanelOpenClose : MonoBehaviour{

    public GameObject openpanel;
    public GameObject closepanel0;
    public GameObject closepanel1;
    public GameObject closepanel2;
    public GameObject closepanel3;
    public GameObject closepanel4;
    public GameObject closepanel5;
    public GameObject closepanel6;
    public GameObject closepanel7;
    public GameObject closepanel8;
    public GameObject closepanel9;

    public void OnClick() {
        if (closepanel0 != null) {
            closepanel0.SetActive(false);
        }
        if (closepanel1 != null) {
            closepanel1.SetActive(false);
        }
        if (closepanel2 != null) {
            closepanel2.SetActive(false);
        }
        if (closepanel3 != null) {
            closepanel3.SetActive(false);
        }
        if (closepanel4 != null) {
            closepanel4.SetActive(false);
        }
        if (closepanel5 != null) {
            closepanel5.SetActive(false);
        }
        if (closepanel6 != null) {
            closepanel6.SetActive(false);
        }
        if (closepanel7 != null) {
            closepanel7.SetActive(false);
        }
        if (closepanel8 != null) {
            closepanel8.SetActive(false);
        }
        if (closepanel9 != null) {
            closepanel9.SetActive(false);
        }
        if (openpanel != null) {
            openpanel.SetActive(true);
        }
    }
}
