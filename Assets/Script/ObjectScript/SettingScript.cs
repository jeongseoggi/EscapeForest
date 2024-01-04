using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.EditorCoroutines.Editor;
using UnityEngine;
using UnityEngine.UI;

public class SettingScript : MonoBehaviour
{
    public Slider[] slider = new Slider[2];
    public TextMeshProUGUI text;
    public GameObject panel;
    public Image gameOver;
    public GameObject escMenu;
    public GameObject volMenu;
    private void Awake()
    {
        UIManager.instance.slider[0] = slider[0];
        UIManager.instance.slider[1] = slider[1];
        UIManager.instance.finalMission = panel;
        UIManager.instance.successText = text;
        UIManager.instance.gameOverImg = gameOver;
        UIManager.instance.esc = escMenu;
        UIManager.instance.SetBtn();
    }
}
