using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SoundTest : MonoBehaviour
{
    public AudioClip walkClip;
    bool isWalk = false;
    public GameObject temp;
    void Update()
    { 
        if(Input.GetKey(KeyCode.W))
        {
            if(!isWalk)
            {
                isWalk = true;
                temp = SoundManager.instance.Pop(walkClip, true);
            }
        }
        else
        {
            if(isWalk)
            {
                isWalk = false;
                SoundManager.instance.ReturnPool(temp);
            }
        }

        if(Input.GetKey(KeyCode.LeftShift))
        {
            if(isWalk)
            {
                temp.GetComponent<AudioSource>().pitch = 2;
            }
        }
        else
        {
            if(isWalk)
            {
                temp.GetComponent<AudioSource>().pitch = 1;
            }

        }
    }
}
