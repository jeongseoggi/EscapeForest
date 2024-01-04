using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            player.interactionImage.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            if(player.isInteracion)
            {
                OnInteraction(player);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            player.interactionImage.gameObject.SetActive(false);
            player.isInteracion = false;
        }
    }

    private void OnInteraction(Player player)
    {
        if (player.invenUI.FoundFinalItem())
        {
            UIManager.instance.RadioImage();
            player.isInteracion = false;
            GameManager.instance.callHeliCop = true;
        }
    }
}
