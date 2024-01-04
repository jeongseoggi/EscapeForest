using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliCopter : MonoBehaviour
{
    public Transform pos;
    Player triggerPlayer;

    private void Awake()
    {
        GameManager.instance.heliCop = this;
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, pos.position, Time.deltaTime);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            triggerPlayer = player;
            triggerPlayer.interactionImage.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(triggerPlayer != null && triggerPlayer.isInteracion)
        {
            GameManager.instance.EndingScene();
            triggerPlayer.isInteracion = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(triggerPlayer != null)
        {
            triggerPlayer.interactionImage.gameObject.SetActive(false);
            triggerPlayer.isInteracion = false;
        }
    }
}
