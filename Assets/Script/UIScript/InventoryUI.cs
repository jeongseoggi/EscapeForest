using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;


[System.Serializable]
public class VerticalSlot
{
    public Slots[] slots = new Slots[5];
}
public class InventoryUI : MonoBehaviour
{
    public Player owner;
    public VerticalSlot[] verticalSlots = new VerticalSlot[3];
    int finalItemCode = 999;

    public void Start()
    {

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                verticalSlots[i].slots[j].ownerInven = this;
            }
        }
    }

    public void AddItem(Item item)
    {
        if(CheckItem(item) == 1)
        {
            for(int i=0;i<3;i++)
            {
                for(int j=0;j<5;j++)
                {
                    if (verticalSlots[i].slots[j].item == null)
                    {
                        if(item is ConsumableItem)
                        {
                            ((ConsumableItem)item).count++;
                        }
                        verticalSlots[i].slots[j].SetItem(item);
                        return;
                    }
                }
            }
        }
    }

    public int CheckItem(Item item)
    {
        for(int i = 0; i <3; i++)
        {
            for(int j = 0; j < 5; j++)
            {
                if (verticalSlots[i].slots[j].item != null)
                {
                    if(item is ConsumableItem)
                    {
                        if (item.name == verticalSlots[i].slots[j].item.name)
                        {
                            ((ConsumableItem)verticalSlots[i].slots[j].item).count++;
                            verticalSlots[i].slots[j].CountItem(((ConsumableItem)verticalSlots[i].slots[j].item).count);
                            return 0;
                        }
                    }
                }
            }
        }
        return 1;
    }

    public bool FoundFinalItem()
    {
        for(int i = 0; i <3; i++) 
        { 
            for(int j = 0; j < 5; j++)
            {
                if (verticalSlots[i].slots[j].item != null)
                {
                    if (verticalSlots[i].slots[j].item.itemCode == finalItemCode)
                    {
                        verticalSlots[i].slots[j].SetItem(null);
                        return true;
                    }
                }
            }
        }
        return false;
    }
}
