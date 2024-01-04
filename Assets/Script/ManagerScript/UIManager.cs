using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : SingleTon<UIManager>
{
    public Slider[] slider;
    public GameObject finalMission;
    public TextMeshProUGUI successText;
    public Button gameStartBTN;
    public Button exitBTN;
    public Image gameOverImg;
    public GameObject esc;
    bool openMenu = false;

    protected void Start()
    {
        slider = new Slider[2];
    }

    public void SetSlider()
    {
        slider[0].maxValue = GameManager.instance.player.MaxHp;
        slider[1].maxValue = GameManager.instance.player.MaxStamina;
    }

    public void UseRadio()
    {
        finalMission.SetActive(true);
    }

    public void RadioImage()
    {
        if (!GameManager.instance.available)
        {
            return;
        }
        StartCoroutine(PrintText());
    }

    public void SetBtn()
    {
        Button[] btn = esc.GetComponentsInChildren<Button>();
        btn[0].onClick.AddListener(GameManager.instance.GoTitle);
    }


    IEnumerator PrintText()
    {
        successText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        successText.gameObject.SetActive(false);
    }

    public void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            if(gameStartBTN == null)
            {
                gameStartBTN = FindObjectOfType<Canvas>().GetComponentsInChildren<Button>()[0];
                exitBTN = FindObjectOfType<Canvas>().GetComponentsInChildren<Button>()[1];
                gameStartBTN.onClick.AddListener(GameManager.instance.ClickStartBtn);
                exitBTN.onClick.AddListener(GameManager.instance.ClickExitBtn);
            }
        }

        if(SceneManager.GetActiveScene().buildIndex == 2)
        {

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if(openMenu == false)
                {
                    openMenu = true;
                    esc.SetActive(true);
                }
                else
                {
                    openMenu = false;
                    esc.SetActive(false);
                }
            }
        }
    }
}
