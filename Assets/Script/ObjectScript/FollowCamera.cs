using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform player;
    public float dist = 10;
    public float height = 5;
    public float damp = 20;

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.position - (player.forward * dist) + (Vector3.up * height), Time.deltaTime * damp);
        transform.LookAt(player);
    }
}
