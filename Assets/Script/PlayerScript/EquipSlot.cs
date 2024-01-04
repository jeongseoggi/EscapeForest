using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipSlot : MonoBehaviour
{
    public Item item;
    public Image image;
    public TextMeshProUGUI nameText;
    public GameObject emptySprite;


    public void SetItem(Item SetItem)
    {
        item = SetItem;
        if(item != null) 
        {
            image.sprite = item.sprite;
            nameText.text = item.name;
        }
        emptySprite.SetActive(item == null);
    }
}
