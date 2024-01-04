using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterDetect : MonoBehaviour
{
    [SerializeField]
    int count = 0;
    [SerializeField]
    Camera cam;
    [SerializeField]
    float prevDist;

    private void Start()
    {
        cam = Camera.main;
        prevDist = cam.GetComponent<FollowCamera>().dist;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            cam.GetComponent<FollowCamera>().dist = 3;
            count++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            if(count >= 2)
            {
                cam.GetComponent<FollowCamera>().dist = prevDist;
                count = 0;
            }

            if (GameManager.instance.available && GameManager.instance.callHeliCop)
            {
                GameManager.instance.HeliCopter();
            }
        }
    }
}
