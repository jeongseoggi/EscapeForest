using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    //비동기로딩
    //작업이 완료될때까지 기다리지 않고 다른 작업을 동시에 수행하는 작업 방식

    public Slider slider;
    public string SceneName = "MainGame";
    private float time = 0;
    public TextMeshProUGUI progressText;
    public TextMeshProUGUI toolTipText;

    string[] tipText = new string[4]
        { "주변의 오브젝트를 부셔서 조합 아이템을 만들 수 있습니다.",
          "최종보스를 잡아 탈출하십시오!",
          "몬스터를 피하거나 싸워서 생존하세요",
          "과일을 이용해 체력을 채울 수 있습니다"
        };

    void Start()
    {
        StartCoroutine(LoadingScene());
        int RandomNum = Random.Range(0, 4);
        toolTipText.text = tipText[RandomNum];
    }

    //AsyncOperation isDone = 비동기 작업의 완료 여부를 확인할 수 있음. 작업이 완료되면 true 완료되지 않았으면 false를 반환
    //AsyncOperation progress = 비동기 작업의 진행 정도 보통 0~1;
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
