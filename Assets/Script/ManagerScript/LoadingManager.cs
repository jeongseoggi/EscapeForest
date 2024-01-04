using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    //�񵿱�ε�
    //�۾��� �Ϸ�ɶ����� ��ٸ��� �ʰ� �ٸ� �۾��� ���ÿ� �����ϴ� �۾� ���

    public Slider slider;
    public string SceneName = "MainGame";
    private float time = 0;
    public TextMeshProUGUI progressText;
    public TextMeshProUGUI toolTipText;

    string[] tipText = new string[4]
        { "�ֺ��� ������Ʈ�� �μż� ���� �������� ���� �� �ֽ��ϴ�.",
          "���������� ��� Ż���Ͻʽÿ�!",
          "���͸� ���ϰų� �ο��� �����ϼ���",
          "������ �̿��� ü���� ä�� �� �ֽ��ϴ�"
        };

    void Start()
    {
        StartCoroutine(LoadingScene());
        int RandomNum = Random.Range(0, 4);
        toolTipText.text = tipText[RandomNum];
    }

    //AsyncOperation isDone = �񵿱� �۾��� �Ϸ� ���θ� Ȯ���� �� ����. �۾��� �Ϸ�Ǹ� true �Ϸ���� �ʾ����� false�� ��ȯ
    //AsyncOperation progress = �񵿱� �۾��� ���� ���� ���� 0~1;
    IEnumerator LoadingScene()
    {
        AsyncOperation oper = SceneManager.LoadSceneAsync(SceneName);  
        oper.allowSceneActivation = false;

        while(!oper.isDone)
        {
            time += Time.deltaTime + Time.deltaTime;
            
            slider.value = time / 10f;
            progressText.text = Mathf.Floor(slider.value * 100).ToString() + "%";

            if(slider.value == 1)
            {
                oper.allowSceneActivation = true;
            }


            yield return null;
        }
    }
}
