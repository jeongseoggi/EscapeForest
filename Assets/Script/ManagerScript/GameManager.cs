using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : SingleTon<GameManager>
{
    public CombineDictionary dic;
    public Player player;
    public bool available; //�������� �׿��� �� ������ ��밡��
    public SpawnManager spawnManager;
    public HeliCopter heliCop;
    public bool callHeliCop;

    protected new void Awake()
    {
        base.Awake();
    }

    public void ClickStartBtn()
    {
        SceneManager.LoadScene("LoadingScene");
    }

    public void ClickExitBtn()
    {
        Application.Quit();
    }

    public void GoTitle()
    {
        SceneManager.LoadScene("TitleScene");
        if(FindObjectOfType<BackGround>() != null)
        {
            SoundManager.instance.ReturnPool(FindObjectOfType<BackGround>().temp);
            FindObjectOfType<BackGround>().gameObject.SetActive(false);
        }
    }

    public void GameStart()
    {
        //�÷��̾� �ʹ� ���� ����
        GameObject obj = Instantiate(Resources.Load<GameObject>("Item/axe_02"));
        obj.transform.SetParent(player.weaponEquip.transform, false);
        player.weaponEquip.GetComponent<weaponSlot>().equipSlot[0] = obj.GetComponent<Item>();
    }

    public void GameEnd()
    {
        available = true;
        UIManager.instance.UseRadio();
    }

    public void EndingScene()
    {
        UIManager.instance.gameOverImg.gameObject.SetActive(true);
        UIManager.instance.gameOverImg.GetComponentInChildren<TextMeshProUGUI>().text = "Successful Escape!";
        StartCoroutine(EndGame());
    }

    public void Die()
    {
        UIManager.instance.gameOverImg.gameObject.SetActive(true);
        UIManager.instance.gameOverImg.GetComponentInChildren<TextMeshProUGUI>().text = "Game Over!";
        StartCoroutine(EndGame());
    }

    public void HeliCopter()
    {
        if (available)
        {
            heliCop.gameObject.SetActive(true);
        }
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(3);
        if (FindObjectOfType<BackGround>() != null)
        {
            SoundManager.instance.ReturnPool(FindObjectOfType<BackGround>().temp);
            FindObjectOfType<BackGround>().gameObject.SetActive(false);
        }
        SceneManager.LoadScene("TitleScene");
    }
}
