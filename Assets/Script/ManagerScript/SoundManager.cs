using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class SoundManager : SingleTon<SoundManager>
{
    public GameObject soundPrafab;
    public Queue<GameObject> pool;

    private void Start()
    {
        pool = new Queue<GameObject>();
        Init();
    }

    void Init()
    {
        for(int i = 0; i < 10; i++)
        {
            GameObject temp = Instantiate(soundPrafab,transform);
            temp.SetActive(false);
            pool.Enqueue(temp);
        }
    }

    public GameObject Pop(AudioClip clip, bool isLoop = false)
    {
        GameObject temp = pool.Dequeue();
        temp.SetActive(true);
        temp.GetComponent<SoundComponent>().Play(clip, isLoop);
        return temp;
    }


    public void ReturnPool(GameObject returnObj)
    {
        returnObj.SetActive(false);
        pool.Enqueue(returnObj);
    }
}
