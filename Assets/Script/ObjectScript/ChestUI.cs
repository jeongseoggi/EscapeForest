using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChestUI : MonoBehaviour
{
    public Chest chest;
    public Player player;
    public ChestSlot[] verSlot = new ChestSlot[3];

    public void ChestItem(Item[] setItems)
    {
        for(int i= 0; i < 3; i++)
        {
            verSlot[i].moveInven = player.invenUI;
            verSlot[i].SetItem(setItems[i]);
        }
    }

    public void Update()
    {
        EmptyChest();
    }

    public void EmptyChest()
    {
        for(int i = 0; i < 3; i++)
        {
            if (verSlot[i].item != null)
            {
                return;
            }
            chest.randomItem[i] = null;
        }
    }

}
