using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackGround : MonoBehaviour
{
    public AudioClip backGroundClip;
    public GameObject temp;


    private void Start()
    {
        temp = SoundManager.instance.Pop(backGroundClip, true);
    }

}
