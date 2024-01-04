using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    Animator animator;
    public Image chestImage;
    public Item[] randomItem = new Item[3];
    public Item[] chestItem = new Item[5];

    void Start()
    {
        animator = GetComponent<Animator>();
        for(int i = 0; i < randomItem.Length; i++)
        {
            randomItem[i] = chestItem[Random.Range(0, 5)];
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            player.interactionImage.gameObject.SetActive(true);
            chestImage.GetComponent<ChestUI>().player = player;
            chestImage.GetComponent<ChestUI>().chest = this;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            if(player.isInteracion)
            {
                player.isInteracion = false;
                animator.SetBool("Open", true);
                chestImage.gameObject.GetComponent<ChestUI>().ChestItem(randomItem);
                chestImage.gameObject.SetActive(true);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.interactionImage.gameObject.SetActive(false);
            chestImage.gameObject.SetActive(false);
            player.isInteracion = false;
        }
        animator.SetBool("Open", false);
    }
}

